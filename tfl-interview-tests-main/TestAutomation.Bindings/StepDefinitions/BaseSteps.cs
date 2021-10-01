using TestAutomation.Bindings.Contexts;
using TestAutomation.PageObjects.Factories;

namespace TestAutomation.Bindings.StepDefinitions
{
    public abstract class BaseSteps 
    {
        protected IPageObjectFactory PageObjectFactory { get; }
        protected PageContext PageContext { get; }

        protected BaseSteps(IPageObjectFactory pageObjectFactory, PageContext pageContext)
        {
            PageObjectFactory = pageObjectFactory;
            PageContext = pageContext;
        }
    }
}
