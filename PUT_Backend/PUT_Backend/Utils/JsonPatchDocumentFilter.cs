using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PUT_Backend
{
    public class JsonPatchDocumentFilter : IDocumentFilter
    {
        public void Apply(OpenApiDocument swaggerDoc, DocumentFilterContext context)
        {
            if (swaggerDoc == null)
            {
                throw new ArgumentNullException(nameof(swaggerDoc));
            }

            // Handle schemas
            if (swaggerDoc.Components?.Schemas != null)
            {
                var keysToRemove = swaggerDoc.Components.Schemas
                    .Where(s => s.Key.StartsWith("SystemTextJsonPatch", StringComparison.OrdinalIgnoreCase))
                    .Select(s => s.Key)
                    .ToList();

                foreach (var key in keysToRemove)
                {
                    swaggerDoc.Components.Schemas.Remove(key);
                }

                // Define the schema for a single patch operation
                var patchOperationSchema = new OpenApiSchema
                {
                    Type = "object",
                    Description = "Describes a single operation in a JSON Patch document.",
                    Properties = new Dictionary<string, OpenApiSchema>
                    {
                        {
                            "op", new OpenApiSchema
                            {
                                Type = "string",
                                Description = "The operation type. Allowed values: 'add', 'remove', 'replace', 'move', 'copy', 'test'.",
                            }
                        },
                        {
                            "path", new OpenApiSchema
                            {
                                Type = "string",
                                Description = "The JSON Pointer path to the property in the target document where the operation is to be applied.",
                            }
                        },
                        {
                             "value", new OpenApiSchema
                            {
                                // Allow multiple types for the "value" field
                                OneOf = new List<OpenApiSchema>
                                {
                                    new OpenApiSchema { Type = "string" },
                                    new OpenApiSchema { Type = "integer" },
                                    new OpenApiSchema { Type = "boolean" },
                                    new OpenApiSchema { Type = "object" },
                                    new OpenApiSchema { Type = "array" }
                                },
                                Description = "The value to apply for 'add', 'replace', or 'test' operations. Not required for 'remove', 'move', or 'copy'.",
                            }
                        },
                    },
                    Required = new HashSet<string> { "op", "path" }
                };

                // Define the schema for an array of patch operations
                swaggerDoc.Components.Schemas.Add("JsonPatchDocument", new OpenApiSchema
                {
                    Type = "array",
                    Description = "A JSON Patch document containing a list of operations.",
                    Items = patchOperationSchema
                });
            }

            // Handle paths
            if (swaggerDoc.Paths != null)
            {
                foreach (var path in swaggerDoc.Paths)
                {
                    if (path.Value?.Operations != null && path.Value.Operations.TryGetValue(OperationType.Patch, out var patchOperation))
                    {
                        if (patchOperation.RequestBody?.Content != null)
                        {
                            foreach (var key in patchOperation.RequestBody.Content.Keys.ToList())
                            {
                                patchOperation.RequestBody.Content.Remove(key);
                            }

                            if (patchOperation.OperationId?.StartsWith("odata", StringComparison.OrdinalIgnoreCase) == true)
                            {
                                path.Value.Operations.Remove(OperationType.Patch);
                            }

                            patchOperation.RequestBody.Content.Add("application/json-patch+json", new OpenApiMediaType
                            {
                                Schema = new OpenApiSchema
                                {
                                    Reference = new OpenApiReference { Type = ReferenceType.Schema, Id = "JsonPatchDocument" },
                                },
                            });
                        }
                    }
                }
            }
        }
    }
}