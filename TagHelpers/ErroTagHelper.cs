﻿using Microsoft.AspNetCore.Razor.TagHelpers;

namespace ProjetoFourTask.TagHelpers
{
    public class ErroTagHelper : TagHelper
    {
        public string Texto { get; set; }
        public string Class { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            if (!string.IsNullOrEmpty(Texto))
            {
                output.TagName = "div";

                if (string.IsNullOrEmpty(Class))
                {
                    output.Attributes.SetAttribute("class", "alert alert-danger alert-dismissible fade show");
                }
                else
                {
                    output.Attributes.SetAttribute("class", Class);
                }
                output.Content.SetContent(Texto);
                output.PostContent.AppendHtml("<button class='btn-close' data-bs-dismiss='alert'></button>");
            }
        }
    }
}
