﻿using Microsoft.OpenApi.Models;
using PrincipleStudios.OpenApi.CSharp;
using PrincipleStudios.OpenApi.Transformations;
using Snapshooter.Xunit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using static PrincipleStudios.OpenApiCodegen.Server.Mvc.DocumentHelpers;

namespace PrincipleStudios.OpenApiCodegen.Server.Mvc
{
    public class CSharpPathControllerTransformerShould
    {
        [Theory]
        [InlineData(0, "/pets")]
        [InlineData(0, "/pets/{id}")]
        [InlineData(1, "/pet")]
        [InlineData(1, "/pet/findByStatus")]
        [InlineData(1, "/pet/findByTags")]
        [InlineData(1, "/pet/{petId}/uploadImage")]
        [InlineData(1, "/store/inventory")]
        [InlineData(1, "/store/order")]
        [InlineData(1, "/store/order/{orderId}")]
        [InlineData(1, "/user")]
        [InlineData(1, "/user/createWithArray")]
        [InlineData(1, "/user/createWithList")]
        [InlineData(1, "/user/login")]
        [InlineData(1, "/user/logout")]
        [InlineData(1, "/user/{username}")]
        public void TransformController(int documentId, string path)
        {
            var documentName = GetDocumentName(documentId);
            var document = GetDocument(documentId);

            var target = ConstructTarget(document);
            OpenApiTransformDiagnostic diagnostic = new();

            var result = target.TransformController(path, document.Paths[path], diagnostic);

            Snapshot.Match(result.SourceText, $"{nameof(CSharpPathControllerTransformerShould)}.{nameof(TransformController)}.{CSharpNaming.ToTitleCaseIdentifier(documentName)}.{CSharpNaming.ToTitleCaseIdentifier(path)}");
        }

        private static IOpenApiPathControllerTransformer ConstructTarget(OpenApiDocument document, string baseNamespace = "PrincipleStudios.Test")
        {
            return new CSharpPathControllerTransformer(document, baseNamespace);
        }

    }
}