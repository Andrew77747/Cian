using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;

namespace Cian.Framework.Tools
{
    public class SeleniumWrapper
    {
        private readonly IWebDriver _driver;
        private readonly WebDriverWait _wait;
        private readonly WebDriverWait _customDriverWait;
        private readonly WebDriverWait _waitForAlert;

        public SeleniumWrapper(IWebDriver driver, WebDriverWait wait)
        {
            _driver = driver;
            _wait = wait;
            _customDriverWait = new WebDriverWait(driver, TimeSpan.FromSeconds(15));
            _waitForAlert = new WebDriverWait(driver, TimeSpan.FromSeconds(1));
        }

        #region Actions

        //Перейти по URL
        public void NavigateToUrl(string url)
        {
            _driver.Navigate().GoToUrl(url);
        }

        /// <summary>
        /// Перезагрузить страницу
        /// </summary>
        public void RefreshPage()
        {
            _driver.Navigate().Refresh();
        }

        //Перейти назад на странице
        public void NavigateBack()
        {
            _driver.Navigate().Back();
        }

        //Найти элемент
        public IWebElement FindElement(By by)
        {
            WaitElementDisplayed(by);
            return _driver.FindElement(by);
        }

        //Найти элементы
        public List<IWebElement> FindElements(By by)
        {
            //WaitElementDisplayed(by);
            return new List<IWebElement>(_driver.FindElements(by));
        }

        //Кликнуть по элементу
        public void ClickElement(By by)
        {
            FindElement(by).Click();
        }

        //Кликнуть по первому видимому элементу
        public void ClickElementFirstOrDefault(By by)
        {
            var elements = FindElements(by);
            elements.FirstOrDefault()?.Click();
        }

        //Кликнуть по первому видимому элементу из списка. Поиск по локатору
        public void ClickElementDisplayed(By by)
        {
            var elements = FindElements(by);

            foreach (var element in elements)
            {
                if (!IsElementDisplayed(element)) continue;
                element.Click();
                break;

                //if (IsElementDisplayed(element))
                //{
                //    element.Click();
                //    break;
                //}
            }
        }

        //Кликнуть по первому видимому элементу из списка. Веб элемент
        public void ClickElementDisplayed(List<IWebElement> elements)
        {
            foreach (var element in elements)
            {
                if (!IsElementDisplayed(element)) continue;
                element.Click();
                break;
            }
        }

        //Выбрать пункт из селекта по имени по локатору
        public void SelectDropdownMenuByName(By by, string selectItemName)
        {
            IWebElement selectElem = FindElement(by);
            SelectElement select = new SelectElement(selectElem);
            //var options = select.Options;
            select.SelectByText(selectItemName);
        }

        //Выбрать пункт из селекта по имени у веб-элемента
        public void SelectDropdownMenuByName(IWebElement element, string selectItemName)
        {
            SelectElement select = new SelectElement(element);
            //var options = select.Options;
            select.SelectByText(selectItemName);
        }

        //Выбрать пункт из селекта по значению (value)
        public void SelectDropdownMenuByValue(By by, string selectItemValue)
        {
            IWebElement selectElem = FindElement(by);
            SelectElement select = new SelectElement(selectElem);
            //var options = select.Options;
            select.SelectByValue(selectItemValue);
        }

        //Проверяем, что элемент выбран в дропдауне
        public bool IsElementSelected(By by)
        {
            try
            {
                return _driver.FindElement(by).Selected;
            }

            catch (NoSuchElementException)
            {
                return false;
            }
        }

        //Найти элемент и напечатать текст в нем по локатору
        public void TypeAndSend(By by, string text)
        {
            FindElement(by).SendKeys(text);
        }

        //Найти элемент и напечатать текст в нем (веб элемент)
        public void TypeAndSend(IWebElement elem, string text)
        {
            elem.SendKeys(text);
        }

        //Очистить инпут и ввести текст
        public void ClearTypeAndSend(By by, string text)
        {
            _driver.FindElement(by).Clear();
            _driver.FindElement(by).SendKeys(text);
            //_driver.FindElement(by).SendKeys(Keys.Control + text);
        }

        //Очистить инпут
        public void ClearText(By by)
        {
            _driver.FindElement(by).Clear();
        }

        //Найти элемент, напечатать в текст в нем и нажать Enter
        public void TypeAndSendWithEnter(By by, string text)
        {
            FindElement(by).SendKeys(text + Keys.Enter);
        }

        //Найти элемент и нажать Enter
        public void PutEnter(By by)
        {
            FindElement(by).SendKeys(Keys.Enter);
        }

        //Нажать Esc без привязки к элементу
        public void SendEscape()
        {
            Actions action = new Actions(_driver);
            action.SendKeys(Keys.Escape).Build().Perform();
        }

        //Найти элемент, напечать текст в нем и сбросить фокус с инпута 
        public void SendKeysWithEscape(By by, string text)
        {
            FindElement(by).SendKeys(text + Keys.Escape);
        }

        //Получить текущий URL
        public string GetUrl()
        {
            return _driver.Url;
        }

        //Переключиться во Frame по индексу (номеру)
        public void SwitchToFrame(int x)
        {
            _driver.SwitchTo().Frame(x);
        }

        //Переключиться во Frame по локатору
        public void SwitchToFrameByName(By by)
        {
            _driver.SwitchTo().Frame(FindElement(by));
        }

        //Переключиться на основной frame
        public void SwitchToDefaultFrame()
        {
            _driver.SwitchTo().DefaultContent();
            //_driver.SwitchTo().ParentFrame();
        }

        //Открыть новую вкладку
        public void OpenNewTabOrWindow(WindowType type)
        {
            _driver.SwitchTo().NewWindow(type);
        }

        //Переключиться на Alert
        public void SwitchToAlert()
        {
            //IAlert alert = _driver.SwitchTo().Alert();
            //alert.Accept();
            _driver.SwitchTo().Alert().Accept();
        }

        //Переключиться на Alert и нажать OK
        public void SwitchToAlertAccept()
        {
            //IAlert alert = _driver.SwitchTo().Alert();
            //alert.Accept();
            _driver.SwitchTo().Alert().Accept();
        }

        //Переключиться на Alert и нажать Dismiss
        public void SwitchToAlertDismiss()
        {
            _driver.SwitchTo().Alert().Dismiss();
        }

        //Подождать алерт и закрыть его
        public void WaitAlertAndClose()
        {
            try
            {
                while (_waitForAlert.Until(d => IsAlertPresent()))
                {
                    SwitchToAlertDismiss();
                }
            }
            catch (Exception)
            {
            }
        }

        //Получить идентификатор текущего окна
        public string GetCurrentWindowId()
        {
            return _driver.CurrentWindowHandle;
        }

        //Получить количество открытых окон
        public int GetOpenWindowCount()
        {
            return _driver.WindowHandles.Count;
        }

        //Переключиться на другую вкладку (окно)
        public void SwitchToAnotherTab(int numberOfTab)
        {
            var tabs = new List<String>(_driver.WindowHandles);
            _driver.SwitchTo().Window(tabs[numberOfTab]);
        }

        //Получить список открытых окон
        public List<String> GetWindowsList()
        {
            return new List<String>(_driver.WindowHandles);
        }

        //Переключиться на новое окно по номеру
        public void SwitchWindowByNumber(int windowNumber)
        {
            _driver.SwitchTo().Window(_driver.WindowHandles[windowNumber]);
        }

        //Переключиться на новое окно по ID (по имени)
        public void SwitchWindowByName(string windowName)
        {
            _driver.SwitchTo().Window(windowName);
        }

        //Переключиться на новое окно (на последнее)
        public void SwitchWindowLast()
        {
            _driver.SwitchTo().Window(_driver.WindowHandles.Last());
        }

        //Закрыть текущее окно
        public void CloseCurrentWindow()
        {
            _driver.Close();
        }

        //Показать Url и title текущей страницы
        public string GetPageUrlAndTitle()
        {
            return (_driver.Title + _driver.Url);
        }

        //Показать Url текущей страницы
        public string GetPageUrl()
        {
            return _driver.Url;
        }

        //Показать title текущей страницы
        public string GetPageTitle()
        {
            return _driver.Title;
        }

        //Загрузить файл
        public void UploadFiles(By selector, string path)
        {
            _driver.FindElement(selector).SendKeys(path);
        }

        //Проскроллить до элемента, чтобы тот был в зоне видимости
        public void ScrollIntoViewElement(By by)
        {
            //IJavaScriptExecutor js = (IJavaScriptExecutor)_driver;
            //js.ExecuteScript("arguments[0].scrollIntoView();", element);

            var e = _driver.FindElement(by);
            // JavaScript Executor to scroll to element
            ((IJavaScriptExecutor)_driver)
                .ExecuteScript("arguments[0].scrollIntoView(true);", e);
            //Console.WriteLine(e.Text);
        }

        //Проскроллить страницу в самый низ
        public void ScrollTillDown()
        {
            //IJavaScriptExecutor js = (IJavaScriptExecutor)_driver;
            //js.ExecuteScript("arguments[0].scrollIntoView();", element);

            //var e = _driver.FindElement(by);
            // JavaScript Executor to scroll to element
            ((IJavaScriptExecutor)_driver)
                .ExecuteScript("window.scrollTo(0, document.body.scrollHeight);");
            //Console.WriteLine(e.Text);
        }

        //Поставить галочку у чек-бокса, если он не активен
        public void ClickCheckbox(By selector, string value)
        {
            var x = GetValuesOfAttribute(selector, value);
            if (!x.Contains("active"))
            {
                ClickElement(selector);
            }
        }

        //Снять выделение с чек-бокса, если оно присутствует по локатору
        public void UncheckCheckbox(By locator)
        {

            var checkboxAccounts = FindElements(locator);

            foreach (var checkbox in checkboxAccounts)
            {
                if (IsAttributePresent(checkbox, "checked"))
                {
                    checkbox.Click();
                }
            }
        }

        //Снять выделение с чек-бокса, если оно присутствует
        public void UncheckCheckbox(List<IWebElement> elementsList)
        {
            foreach (var checkbox in elementsList)
            {
                if (IsAttributePresent(checkbox, "checked"))
                {
                    checkbox.Click();
                }
            }
        }

        //Подсчет элементов в списке
        public int GetElementCount(By selector)
        {
            return _driver.FindElements(selector).Count;
        }

        //Получить текст элемента
        public string GetElementText(By selector)
        {
            string name = FindElement(selector).Text;
            return name;
        }

        //Получить HTML страницы
        public string GetUserName()
        {
            return _driver.PageSource;
        }

        //Навестись на элемент по текстовому селектору
        public void HoverMouseOnElement(string selector)
        {
            try
            {
                Actions action = new Actions(_driver);
                action.MoveToElement(FindElement(By.XPath(selector))).Build().Perform();
            }
            catch (NoSuchElementException e)
            {
                Console.WriteLine("There is no such element! Error: " + e);
            }
        }

        //Стирать символы по одному
        public void DeleteSymbolsByOne(IWebElement elem)
        {
            Actions action = new Actions(_driver);
            action.KeyDown(elem, Keys.Backspace).KeyUp(elem, Keys.Backspace).Perform();
        }

        //Навестись на элемент по селектору
        public void HoverMouseOnElement(By selector)
        {
            try
            {
                Actions action = new Actions(_driver);
                action.MoveToElement(FindElement(selector)).Build().Perform();
            }
            catch (NoSuchElementException e)
            {
                Console.WriteLine("There is no such element! Error: " + e);
            }
        }

        //Навестись на веб-элемент
        public void HoverMouseOnElement(IWebElement element)
        {
            try
            {
                Actions action = new Actions(_driver);
                action.MoveToElement(element).Build().Perform();
            }
            catch (NoSuchElementException e)
            {
                Console.WriteLine("There is no such element! Error: " + e);
            }
        }

        //Захватить мышкой элемент и передвинуть его
        public void MoveElement(By selector, int x, int y)
        {
            try
            {
                Actions action = new Actions(_driver);
                action.MoveToElement(FindElement(selector)).ClickAndHold().MoveByOffset(x, y).Release().Perform();
            }
            catch (NoSuchElementException e)
            {
                Console.WriteLine("There is no such element! Error: " + e);
            }
        }

        //Захватить мышкой веб-элемент и передвинуть его
        public void MoveElement(IWebElement element, int x, int y)
        {
            try
            {
                Actions action = new Actions(_driver);
                action.MoveToElement(element).ClickAndHold().MoveByOffset(x, y).Release().Perform();
            }
            catch (NoSuchElementException e)
            {
                Console.WriteLine("There is no such element! Error: " + e);
            }
        }

        //Собрать все вэб элементы по одному локатору в лист 
        public List<IWebElement> GetElements(By selector)
        {
            return _driver.FindElements(selector).ToList();
        }

        //Собрать все вэб элементы по одному локатору в лист и получить у каждого из них текст в лист. Самый короткий способ с Linq
        public List<string> GetElementsTextListLinq(By selector)
        {
            return FindElements(selector).Select(x => x.Text).ToList();
        }

        //Собрать все вэб элементы по одному локатору в лист и получить у каждого из них текст в лист. Второй короткий способ с Linq
        public List<string> GetElementsTextListLinq2(By selector) => FindElements(selector).Select(x => x.Text).ToList();


        //Собрать все вэб элементы по одному локатору в лист и получить у каждого из них текст в другой лист 
        public List<string> GetElementsTextList(By selector)
        {
            var allElementsWithText = FindElements(selector);
            List<string> names = new();

            foreach (var oneElementWithText in allElementsWithText)
            {
                names.Add(oneElementWithText.Text);
            }

            return names;
        }

        //Собрать все вэб элементы по одному локатору в лист, найти у каждого дочерний элемент и получить у каждого из них текст в другой лист
        public List<string> GetElementsTextList(By selector, By selector2)
        {
            var allElementsWithText = FindElements(selector);
            List<string> names = new();

            foreach (var oneElementWithText in allElementsWithText)
            {
                names.Add(oneElementWithText.FindElement(selector2).Text);
            }

            return names;
        }

        //Собрать все вэб элементы по одному локатору в лист и получить у каждого из них текст в массив + вывести на экран весь текстовый массив
        public string[] GetElementsTextArray(By selector)
        {
            var allElementsWithText = FindElements(selector);
            string[] names = new string[allElementsWithText.Count];

            for (int i = 0; i < names.Length; i++)
            {
                names[i] = allElementsWithText[i].Text;
                Console.WriteLine(names[i]);
            }

            return names;
        }

        //Собрать все вэб элементы по одному локатору в лист, найти у каждого дочерний элемент и получить у каждого из них текст в массив + вывести на экран весь текстовый массив
        public string[] GetElementsTextArray(By selector, By selector2)
        {
            var allElementsWithText = GetElements(selector);
            string[] names = new string[allElementsWithText.Count];

            for (int i = 0; i < names.Length; i++)
            {
                names[i] = allElementsWithText[i].FindElement(selector2).Text;
                Console.WriteLine(names[i]);
            }

            return names;
        }

        //Получить значение аттрибута по локатору (value - имя аттрибута, у которого получаем значение)
        public string GetValuesOfAttribute(By by, string value)
        {
            return _driver.FindElement(by).GetAttribute(value);
        }

        //Получить значение аттрибута веб элемента (value - имя аттрибута, у которого получаем значение)
        public string GetValuesOfAttribute(IWebElement element, string value)
        {
            return element.GetAttribute(value);
        }

        //Получить список занчений атрибутов
        public string[] GetValuesOfAttributeList(By selector, string value, By selector2)
        {
            //var allElementsWithText = GetElements(selector);
            //string[] names = new string[allElementsWithText.Count];
            List<IWebElement> ElementsWithNamesAttribute = GetElements(selector);
            string[] names = new string[ElementsWithNamesAttribute.Count];
            //List<string> names = new List<string>();

            for (int i = 0; i < names.Length; i++)
            {
                names[i] = GetValuesOfAttribute(selector2, value);
                Console.WriteLine(names[i]);
            }

            //foreach (var name in names)
            //{
            //    name = GetValuesOfAttribute(selector, value);
            //}

            return names;
        }

        //Перевести все значения массива из string в int и получить сумму всех чисел
        public int TotalAmount(string[] actualArray)
        {
            //Обрезка каких-либо символов в массиве, из-за которых мы не сможем конвертировать в int
            //for (int i = 0; i < actualArray.Length; i++)
            //{
            //    actualArray[i] = actualArray[i].Replace(" ", string.Empty);
            //    actualArray[i] = actualArray[i].Replace("₽", string.Empty);
            //}

            int[] intActualArray = new int[actualArray.Length];

            for (int i = 0; i < actualArray.Length; i++)
            {
                intActualArray[i] = int.Parse(actualArray[i]);
            }

            int totalAmount = 0;

            for (int i = 0; i < actualArray.Length; i++)
            {
                totalAmount += intActualArray[i];
            }

            return totalAmount;
        }

        //Образать часть текста. Обрезаем текст в начале и в конце и оставляем в середине. Работает по целым словам
        public string CutPartTextFromMiddleWithAllTextValue(string actualText, string deleteTextBefore, string deleteTextAfter)
        {
            Match match = Regex.Match(actualText, $@"{deleteTextBefore}(.*?){deleteTextAfter}");

            string x = match.Groups[1].Value;
            return x;
        }

        //Обрезать первую часть текста в начале
        public string CutFirstPartTextWithAllTextValue(string actualText, string deleteTextBefore)
        {
            Match match = Regex.Match(actualText, $@"{deleteTextBefore}(.*)");
            return match.Groups[1].Value;
        }

        //Обрезать последнюю часть текста в конце
        public string CutLastPartTextWithAllTextValue(string actualText, string deleteTextAfter)
        {
            Match match = Regex.Match(actualText, $@"(.*){deleteTextAfter}");
            return match.Groups[1].Value;
        }

        //Образать часть текста. Обрезаем текст в начале и в конце и оставляем в середине. Работает только по символам
        public string CutPartTextFromMiddle(string actualText, string deleteBefore, string deleteAfter)
        {
            Match match = Regex.Match(actualText, $@"(?<={deleteBefore})[^{deleteAfter}]*");
            return match.Value;
        }

        #endregion

        #region Validation

        //Проверка присутствия алерта
        public bool IsAlertPresent()
        {
            try
            {
                _driver.SwitchTo().Alert();
                return true;
            }
            catch (NoAlertPresentException)
            {
                return false;
            }
        }

        //Провекра, что аттрибут существует у элемента
        public bool IsAttributePresent(By by, string attributeName)
        {
            bool result = false;

            try
            {
                string attributeValue = FindElement(by).GetAttribute(attributeName);
                if (attributeValue != null)
                    result = true;
            }
            catch (Exception)
            {
                return result;
            }

            return result;
        }

        //Провекра, что аттрибут существует у веб элемента 
        public bool IsAttributePresent(IWebElement element, string attributeName)
        {
            bool result = false;

            try
            {
                string attributeValue = element.GetAttribute(attributeName);
                if (attributeValue != null)
                    result = true;
            }
            catch (Exception)
            {
                return result;
            }

            return result;
        }

        //Проверка, что значение атрибута value равно искомому занчению (для полей ввода)
        public bool IsAttributeValueEqual(By selector, string text)
        {
            if (GetValuesOfAttribute(selector, "value").Equals(text))
                return true;
            return false;
        }

        //Проверка, что значение атрибута равно искомому занчению
        public bool IsAttributeValueEqual(By selector, string attributeName, string text)
        {
            if (GetValuesOfAttribute(selector, attributeName).Equals(text))
                return true;
            return false;
        }

        //Проверка, что атрибут содержит часть искомого значения (поиск элемента по локатору)
        public bool IsAttributeContainsValue(By selector, string attributeName, string text)
        {
            if (GetValuesOfAttribute(selector, attributeName).Contains(text))
                return true;
            return false;
        }

        //Проверка, что атрибут содержит часть искомого значения (проверяем веб элемент)
        public bool IsAttributeContainsValue(IWebElement elem, string attributeName, string text)
        {
            //if (GetValuesOfAttribute(elem, attributeName).Contains(text))
            //    return true;
            //return false;

            var x = GetValuesOfAttribute(elem, attributeName);
            return x.Contains(text);
        }

        //Проверка, что атрибут не содержит часть искомого значения (поиск элемента по локатору)
        public bool IsAttributeNotContainsValue(By selector, string attributeName, string text)
        {
            //var x = GetValuesOfAttribute(element, attributeName);
            //if (x.Contains(text))
            //    return false;
            //return true;
            if (!GetValuesOfAttribute(selector, attributeName).Contains(text))
                return true;
            return false;
        }

        //Проверка, что атрибут не содержит часть искомого значения (проверяем веб элемент)
        public bool IsAttributeNotContainsValue(IWebElement element, string attributeName, string text)
        {
            //var x = GetValuesOfAttribute(element, attributeName);
            //if (x.Contains(text))
            //    return false;
            //return true;
            if (!GetValuesOfAttribute(element, attributeName).Contains(text))
                return true;
            return false;
        }

        //Проверяем, что страница загружена и элементы отображаются на странице 
        public bool IsPageLoaded(By selector1, By selector2, By selector3)
        {
            return IsElementDisplayed(selector1) &&
                   IsElementDisplayed(selector2) &&
                   IsElementDisplayed(selector3);
        }

        //Проверяем, что страница загружена, элементы отображаются на странице и навание страницы соответствующее 
        public bool IsPageLoaded(By textSelector, string pageName, By selector1, By selector2, By selector3)
        {
            return IsTextContains(textSelector, pageName) &&
                   IsElementDisplayed(selector1) &&
                   IsElementDisplayed(selector2) &&
                   IsElementDisplayed(selector3);
        }

        //Проверка, что элемент Displayed с кастомным таймаутом, который можно регулировать в поле _customDriverWait
        public bool IsElementDisplayedWithCustomWait(By by)
        {
            try
            {
                _customDriverWait.Until(d => IsElementDisplayed(by));
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
        }

        //Проверяем, что элемент Enabled
        public bool IsElementEnabled(By by)
        {
            try
            {
                return _driver.FindElement(by).Enabled;
            }
            catch (NullReferenceException)
            {
                return false;
            }
        }

        //Проверяем, что элементов в списке стало больше на один (нужно складывать в лист элементы до того, как считать и сравнивать, порпавить при надобности)
        public bool IsElementsIncreasedByOne(By by)
        {
            var listOfElement = FindElements(by);

            return _wait.Until(d => FindElements(by).Count == listOfElement.Count + 1);
        }

        //Проверяем, что элементов в списке стало больше на один (нужно складывать в лист элементы до того, как считать и сравнивать, порпавить при надобности)
        public bool IsElementsDecreasedByOne(By by)
        {
            var listOfElement = FindElements(by);

            return _wait.Until(d => FindElements(by).Count == listOfElement.Count - 1);
        }

        //Проверка, что элементов стало больше
        public bool IsElementsIncreased(int countBefore, By by)
        {
            return _wait.Until(d => FindElements(by).Count > countBefore);
        }

        //Проверка, что элементов стало меньше
        public bool IsElementsDecreased(int countBefore, By by)
        {
            return _wait.Until(d => FindElements(by).Count < countBefore);
        }

        //Проверка, что элемент существует в дом
        public bool IsElementExists(By by)
        {
            try
            {
                _driver.FindElement(by);
                return true;
            }

            catch (NoSuchElementException)
            {
                return false;
            }
        }

        //Проверка, что элементы существует в дом
        public bool IsElementsExist(By by)
        {
            return _driver.FindElements(by).Count > 0;
        }

        //Проверяем, что элемент видимый по локатору
        public bool IsElementDisplayed(By by)
        {
            try
            {
                return _driver.FindElement(by).Displayed;
            }

            catch (NoSuchElementException)
            {
                return false;
            }
        }

        //Проверяем, что элемент видимый (веб элемент)
        public bool IsElementDisplayed(IWebElement element)
        {
            try
            {
                return element.Displayed;
            }

            catch (NoSuchElementException)
            {
                return false;
            }
        }

        //Проверка, что элемент существует и видимый по локатору
        public bool IsElementExistsAndDisplayed(By by)
        {
            ReadOnlyCollection<IWebElement> elements = _driver.FindElements(by);
            if (elements.Count > 0 && elements[0].Displayed)
            {
                return true;
            }
            return false;
        }

        //Проверка, что элемент существует и видимый
        public bool IsElementExistsAndDisplayed(List<IWebElement> elements)
        {
            if (elements.Count > 0 && elements[0].Displayed)
            {
                return true;
            }
            return false;
        }

        //Проверяем, что элемент виден и Enabled
        public bool IsElementDisplayedAndEnabled(By by)
        {
            return
                IsElementDisplayed(by) &&
                IsElementEnabled(by);
        }

        //Проверка, что элемент не виден (веб элемент)
        public bool IsElementNotDisplayed(IWebElement element)
        {
            bool result = false;

            try
            {
                if (!element.Displayed)
                {
                    result = true;
                }
            }
            catch (NoSuchElementException)
            {
                result = true;
            }

            return result;
        }

        //Проверка, что элемент не виден по локатору
        public bool IsElementNotDisplayed(By by)
        {
            bool result = false;

            try
            {
                IWebElement element = _driver.FindElement(by);

                if (!element.Displayed)
                {
                    result = true;
                }
            }
            catch (NoSuchElementException)
            {
                result = true;
            }

            return result;
        }

        //Проверка, что элементы не видны
        public bool IsElementsNotDisplayed(By by)
        {
            ReadOnlyCollection<IWebElement> elements = _driver.FindElements(by);
            if (elements.Count == 0 || !elements[0].Displayed)
            {
                return true;
            }
            return false;
        }

        //Проверка, что элементов не существует в дом
        public bool IsElementsNotExist(By by)
        {
            ReadOnlyCollection<IWebElement> elements = _driver.FindElements(by);
            if (elements.Count == 0)
            {
                return true;
            }
            return false;
        }

        //Проверка, что текст существует на странице
        public bool IsTextExists(string text)
        {
            try
            {
                FindElement(By.XPath($"//*[contains(text(), '{text}')]"));
                Console.WriteLine($"Текст {text} найден на странице");
                return true;
            }
            catch (Exception)
            {
                Console.WriteLine($"Текст {text} не найден на странцие");
                return false;
            }
        }

        //Проверяем, что текст содержит часть нужного текста
        public bool IsTextContains(By by, string text)
        {
            var textFromElement = FindElement(by).Text;
            return textFromElement.Contains(text);
        }

        public bool IsTextNotContains(By by)
        {
            var textFromElement = FindElement(by).Text;
            return !textFromElement.Contains("");
        }

        //Проверяем, что текст равен нужному тексту
        public bool IsTextEqual(By by, string text)
        {
            var textFromElement = FindElement(by).Text;
            return textFromElement.Equals(text);
        }

        //Проверка, что часть текст из одного элемента содержит другой элемент
        public bool IsTextContainsInAnotherElem(By fullTextSelector, By searchedTextSelector)
        {
            return GetElementText(fullTextSelector).Contains(GetElementText(searchedTextSelector));
        }

        //Проверка, что текст из одного элемента равен тексту из другого элемента
        public bool IsTextEqual(By fullTextSelector, By searchedTextSelector)
        {
            return GetElementText(fullTextSelector).Equals(GetElementText(searchedTextSelector));
        }

        //Проверить сортировку массива Asc
        public bool IsSortingAskRight(string[] actualArray)
        {
            string[] expectedArray = new string[actualArray.Length];
            actualArray.CopyTo(expectedArray, 0);

            Array.Sort(expectedArray);

            if (actualArray.SequenceEqual(expectedArray))
            {
                Console.WriteLine(expectedArray);
                return true;
            }
            else
            {
                Console.WriteLine(expectedArray);
                return false;
            }
        }

        //Проверить сортировку массива Desc
        public bool IsSortingDescRight(string[] actualArray)
        {
            string[] expectedArray = new string[actualArray.Length];
            actualArray.CopyTo(expectedArray, 0);

            Array.Sort(expectedArray);
            Array.Reverse(expectedArray);

            if (actualArray.SequenceEqual(expectedArray))
            {
                Console.WriteLine(expectedArray);
                return true;
            }
            else
            {
                Console.WriteLine(expectedArray);
                return false;
            }
        }

        //Перевести стринговый массив в int и проверить сортировку Asc
        public bool IsSortingPriceAskRightStringToInt(string[] actualArray)
        {
            //Если нужно удалить лишние символы, которые будут мешать конвертировать string в int
            //for (int i = 0; i < actualArray.Length; i++)
            //{
            //    actualArray[i] = actualArray[i].Replace(" ", string.Empty);
            //    actualArray[i] = actualArray[i].Replace("₽", string.Empty);
            //}

            int[] expectedArray = new int[actualArray.Length];

            int[] intActualArray = new int[actualArray.Length];

            for (int i = 0; i < actualArray.Length; i++)
            {
                intActualArray[i] = int.Parse(actualArray[i]);
            }

            intActualArray.CopyTo(expectedArray, 0);

            Array.Sort(expectedArray);

            if (intActualArray.SequenceEqual(expectedArray))
            {
                Console.WriteLine(expectedArray);
                return true;
            }
            else
            {
                Console.WriteLine(expectedArray);
                return false;
            }
        }

        //Перевести стринговый список в int и проверить сортировку Asc
        public bool IsSortingPriceAskRightStringToIntList(List<string> actualList)
        {
            //Если нужно удалить лишние символы, которые будут мешать конвертировать string в int
            //for (int i = 0; i < actualArray.Length; i++)
            //{
            //    actualArray[i] = actualArray[i].Replace(" ", string.Empty);
            //    actualArray[i] = actualArray[i].Replace("₽", string.Empty);
            //}

            var expectedList = new List<int>();

            var intActualList = new List<int>();

            foreach (var elem in actualList)
            {
                intActualList.Add(int.Parse(elem));
            }

            expectedList = intActualList.ToList();

            expectedList.Sort();

            if (intActualList.SequenceEqual(expectedList))
            {
                Console.WriteLine(expectedList);
                return true;
            }
            else
            {
                Console.WriteLine(expectedList);
                return false;
            }
        }

        #endregion

        #region Waiters

        //Ждем определенный интервал
        public void WaitInterval(int interval = 2000)
        {
            Thread.Sleep(interval);
        }

        //Ждем определенный интервал
        public void WaitIntervalWithDelay(int interval = 2000)
        {
            Task.Delay(TimeSpan.FromMilliseconds(interval)).Wait();
        }

        //Ждем появления элемента в Дом
        public void WaitElement(By by)
        {
            _wait.Until(d => IsElementExists(by));
        }

        //Ждем появления видимости элемента Displayed
        public void WaitElementDisplayed(By by)
        {
            WaitElement(by);
            _wait.Until(d => IsElementDisplayed(by));
        }

        //Ждем появления видимости элемента Displayed веб элемент
        public void WaitElementDisplayed(IWebElement element)
        {
            _wait.Until(d => IsElementDisplayed(element));
        }

        //Ждем появления видимости элементов Displayed
        public void WaitForElementsDisplayed(By by)
        {
            _wait.Until(d => IsElementExistsAndDisplayed(by));
        }

        //Ждем пока элементы не исчезнут из Дом
        public void WaitElementNotExists(By selector)
        {
            //_wait.Until(d => d.FindElements(selector).Count == 0);
            _wait.Until(d => IsElementsNotExist(selector));
        }

        //Ждем исчезновения видимости элемента по локатору
        public void WaitForElementNotDisplayed(By by)
        {
            _wait.Until(d => IsElementsNotDisplayed(by));
        }

        //Ждем исчезновения видимости элемента (веб элемент)
        public void WaitForElementNotDisplayed(IWebElement elem)
        {
            _wait.Until(d => IsElementNotDisplayed(elem));
        }

        //Ждем исчезновения элемента Depricated
        public void WaitElementInvisible(By by)
        {
            _wait.Until(ExpectedConditions.InvisibilityOfElementLocated(by));
        }

        //Ждем загрузки страницы
        public void WaitPageLoaded(By textSelector, string pageName, By selector1, By selector2, By selector3)
        {
            _wait.Until(d => IsPageLoaded(textSelector, pageName, selector1, selector2, selector3));
        }

        //Ждем точное число элементов
        public void WaitForElementsCount(By by, int elementsCount)
        {
            _wait.Until(d => FindElements(by).Count.Equals(elementsCount));
        }

        //Ждем увеличения числа элементов
        public void WaitForMoreElements(int countBefore, By by)
        {
            _wait.Until(d => IsElementsIncreased(countBefore, by));
        }

        //Ждем уменьшение числа элементов
        public void WaitForLessElements(int countBefore, By by)
        {
            _wait.Until(d => IsElementsDecreased(countBefore, by));
        }

        //Ждем, что количество элементов увеличится на один
        public void WaitForElementsIncreasedByOne(By by)
        {
            _wait.Until(d => IsElementsIncreasedByOne(by));
        }

        //Ждем, что количество элементов уменьшится на один
        public void WaitForElementsDecreasedByOne(By by)
        {
            _wait.Until(d => IsElementsDecreasedByOne(by));
        }

        //Ждем изменение значения атрибута по локатору
        public void WaitForAttributeContains(By element, string attribute, string value)
        {
            _wait.Until(d => IsAttributeContainsValue(element, attribute, value));
        }

        //Ждем изменение значения атрибута веб элемент
        public void WaitForAttributeContains(IWebElement element, string attribute, string value)
        {
            _wait.Until(d => IsAttributeContainsValue(element, attribute, value));
        }

        //Ждем когда у элемента не будет данного значения атрибута по локатору
        public void WaitForAttributeNotContains(By element, string attribute, string value)
        {
            _wait.Until(d => IsAttributeNotContainsValue(element, attribute, value));
        }

        //Ждем когда у элемента не будет данного значения атрибута по локатору веб элемент
        public void WaitForAttributeNotContains(IWebElement element, string attribute, string value)
        {
            _wait.Until(d => IsAttributeNotContainsValue(element, attribute, value));
        }

        //Ждем появления видимости элементов Displayed с кастомным таймаутом, который настраивается в поле _customDriverWait
        public void WaitElementDisplayedWithCustomWait(By by)
        {
            _customDriverWait.Until(d => IsElementDisplayed(by));
        }

        //Ждем появления алерта
        public void WaitAlert()
        {
            _wait.Until(d => IsAlertPresent());
        }

        //Ждем появления нового окна, по умолчанию ждем появления нового второго окна, когда открыто только одно основное окно
        public void WaitNewWindowOpen(int windowCount = 2)
        {
            _wait.Until(d => _driver.WindowHandles.Count == windowCount);
        }

        //Ждем закрытия нового окна, по умолчанию ждем закрытия второго нового второго окна, которое было открыто
        public void WaitNewWindowClose(int windowCount = 1)
        {
            _wait.Until(d => _driver.WindowHandles.Count == windowCount);
        }

        //Ждем появления нового окна, когда открыто много окон
        public void WaitNewWindowOpenWhenManyOpen(int windowCount)
        {
            _wait.Until(d => _driver.WindowHandles.Count == windowCount + 1);
        }

        //Ждем закрытия нового окна, когда открыто много окон
        public void WaitNewWindowCloseWhenManyOpen(int windowCount)
        {
            _wait.Until(d => _driver.WindowHandles.Count == windowCount - 1);
        }

        //Ждем появления текста в элементе
        public void WaitTextAppear(By by, string text)
        {
            _wait.Until(d => IsTextContains(by, text));
        }

        //Ждем загрузки страницы
        public void WaitForLoadingPage()
        {
            _wait.Until(
                d => ((IJavaScriptExecutor)d).ExecuteScript("return document.readyState").Equals("complete"));
        }


        ////
        public void WaitForFrame(By by)
        {
            _wait.Until(ExpectedConditions.FrameToBeAvailableAndSwitchToIt(by));
        }

        #endregion
    }
}