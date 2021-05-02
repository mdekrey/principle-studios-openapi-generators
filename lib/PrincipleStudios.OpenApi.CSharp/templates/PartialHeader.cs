﻿namespace PrincipleStudios.OpenApi.CSharp.templates
{
    public record PartialHeader(
        string? appName,
        string? appDescription,
        string? version,
        string? infoEmail,
        string codeGeneratorVersionInfo
    );
}