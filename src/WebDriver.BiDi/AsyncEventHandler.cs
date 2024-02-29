using System.Threading.Tasks;

namespace OpenQA.Selenium.BiDi;

public delegate Task AsyncEventHandler<TEventArgs>(TEventArgs e);
