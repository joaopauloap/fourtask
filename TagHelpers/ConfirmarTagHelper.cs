using Microsoft.AspNetCore.Razor.TagHelpers;

namespace ProjetoBanco.TagHelpers
{
    public class ConfirmarTagHelper : TagHelper
    {
        public string Class { get; set; }
        public string Value { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {

            output.TagName = "input";
            output.Attributes.SetAttribute("type", "submit");

            if (string.IsNullOrEmpty(Class))
            {
                output.Attributes.SetAttribute("class", "btn btn-primary");
            }
            else
            {
                output.Attributes.SetAttribute("value", Class);
            }

            if (string.IsNullOrEmpty(Value))
            {
                output.Attributes.SetAttribute("value", "Confirmar");
            }
            else
            {
                output.Attributes.SetAttribute("value", Value);
            }

        }

    }
}
