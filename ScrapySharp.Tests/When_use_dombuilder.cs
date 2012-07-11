// ReSharper disable InconsistentNaming

using NUnit.Framework;
using ScrapySharp.Html.Dom;
using ScrapySharp.Html.Parsing;

namespace ScrapySharp.Tests
{
    [TestFixture]
    public class When_use_dombuilder
    {
        [Test]
        public void When_read_a_simple_tag()
        {
            var sourceCode = " dada\n<div class=\"login box1\" id=\"div1\" data-tooltip=\"salut, �a va?\">login: \n\t romcy</div>";
            var codeReader = new CodeReader(sourceCode);
            var domBuilder = new HtmlDomBuilder(codeReader);

            var element = domBuilder.ReadTagDeclaration();
            Assert.AreEqual(" dada\n", element.InnerText);
            Assert.AreEqual(DeclarationType.TextElement, element.Type);
            
            element = domBuilder.ReadTagDeclaration();
            Assert.AreEqual("div", element.Name);
            Assert.AreEqual(DeclarationType.OpenTag, element.Type);
            Assert.AreEqual(3, element.Attributes.Count);
            Assert.AreEqual("login box1", element.Attributes["class"]);
            Assert.AreEqual("div1", element.Attributes["id"]);
            Assert.AreEqual("salut, �a va?", element.Attributes["data-tooltip"]);
            
            element = domBuilder.ReadTagDeclaration();
            Assert.IsNull(element.Name);
            Assert.AreEqual("login: \n\t romcy", element.InnerText);
            Assert.AreEqual(DeclarationType.TextElement, element.Type);


        }
    }
}

// ReSharper restore InconsistentNaming