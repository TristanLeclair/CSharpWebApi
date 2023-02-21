using Microsoft.OpenApi.Models;
using Unchase.Swashbuckle.AspNetCore.Extensions.Extensions;
using Unchase.Swashbuckle.AspNetCore.Extensions.Options;
using WebApplication1.Data;
using WebApplication1.Utils.Api;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1",
        new OpenApiInfo
        {
            Title = "Dnd API - V1",
            Version = "v1"
        });
    var filePath =
        Path.Combine(AppContext.BaseDirectory, "DndNotionApi.xml");
    c.IncludeXmlComments(filePath);
    c.IncludeXmlCommentsWithRemarks(filePath);
    c.AddEnumsWithValuesFixFilters(o =>
        {
            // add schema filter to fix enums (add 'x-enumNames' for NSwag or its alias from XEnumNamesAlias) in schema
            o.ApplySchemaFilter = true;

            // alias for replacing 'x-enumNames' in swagger document
            o.XEnumNamesAlias = "x-enum-varnames";

            // alias for replacing 'x-enumDescriptions' in swagger document
            o.XEnumDescriptionsAlias = "x-enum-descriptions";

            // add parameter filter to fix enums (add 'x-enumNames' for NSwag or its alias from XEnumNamesAlias) in schema parameters
            o.ApplyParameterFilter = true;

            // add document filter to fix enums displaying in swagger document
            o.ApplyDocumentFilter = true;

            // add descriptions from DescriptionAttribute or xml-comments to fix enums (add 'x-enumDescriptions' or its alias from XEnumDescriptionsAlias for schema extensions) for applied filters
            o.IncludeDescriptions = true;

            // add remarks for descriptions from xml-comments
            o.IncludeXEnumRemarks = true;

            // get descriptions from DescriptionAttribute then from xml-comments
            o.DescriptionSource = DescriptionSources
                .DescriptionAttributesThenXmlComments;

            // new line for enum values descriptions
            // o.NewLine = Environment.NewLine;
            o.NewLine = "\n";

            // get descriptions from xml-file comments on the specified path
            // should use "options.IncludeXmlComments(xmlFilePath);" before
            o.IncludeXmlCommentsFrom(filePath);
        }
    );
});
builder.Services.AddSwaggerGenNewtonsoftSupport();

builder.Services.AddScoped<IGodRepo, ApiGodRepo>();

NotionApiCaller.InitializeClient();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();