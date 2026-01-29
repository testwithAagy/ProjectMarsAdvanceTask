using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System;

namespace ProjectMarsAutomationAdvanceTask.Pages.Components
{
    public class ShareSkillComponent
    {
        private readonly IWebDriver _driver;
        private readonly WebDriverWait _wait;

        public ShareSkillComponent(IWebDriver driver)
        {
            _driver = driver;
            _wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
        }

        
        private By ShareSkillButton => By.XPath("//a[contains(@class,'ui basic green button') and text()='Share Skill']");

        private By TitleInput => By.XPath("//input[@name='title']");
        private By DescriptionInput => By.XPath("//textarea[@name='description']");

        private By CategoryDropdown => By.XPath("//select[@name='categoryId']");
        private By SubCategoryDropdown => By.XPath("//select[@name='subcategoryId']");

        private By TagInput => By.XPath("//input[contains(@class,'ReactTags__tagInputField')]");

        private By ServiceTypeHourly =>
    By.XPath("//label[normalize-space()='Hourly basis service']/ancestor::div[contains(@class,'ui radio checkbox')]");

        private By ServiceTypeOneOff =>
            By.XPath("//label[normalize-space()='One-off service']/ancestor::div[contains(@class,'ui radio checkbox')]");

        private By LocationOnSite =>
    By.XPath("//label[normalize-space()='On-site']/ancestor::div[contains(@class,'ui radio checkbox')]");

        private By LocationOnline =>
            By.XPath("//label[normalize-space()='Online']/ancestor::div[contains(@class,'ui radio checkbox')]");

        private By WorkSampleUpload => By.XPath("//span/i[contains(@class,'plus circle icon')]");
    
        private By ActiveRadio => By.XPath("//input[@name='isActive' and @value='true']");
        private By HiddenRadio => By.XPath("//input[@name='isActive' and @value='false']");      
        private By SaveButton => By.XPath("//input[@value='Save']");
        private By CancelButton => By.XPath("//input[@value='Cancel']");
        private By ToastMessageLocator => By.XPath("//div[contains(@class,'ns-box-inner')]");
        private By TagsLabel => By.Id("requiredField");
        private By CalendarEventTitle => By.XPath("//input[@name='title' and contains(@class,'k-textbox')]");
        private By CalendarEventStart => By.XPath("//input[@name='start' and @data-role='datetimepicker']");
        private By CalendarEventEnd => By.XPath("//input[@name='end' and @data-role='datetimepicker']");
        private By CalendarEventAllDay => By.XPath("//input[@name='isAllDay']");
        private By CalendarEventDescription => By.XPath("//textarea[@name='description' and contains(@class,'k-textbox')]");
        private By CalendarEventOwnerDropdown => By.XPath("//span[contains(@class,'k-dropdown-wrap')]");

        

        private IWebElement WaitAndFind(By locator)
        {
            return _wait.Until(ExpectedConditions.ElementIsVisible(locator));
        }

        private void ScrollIntoView(By locator)
        {
            var element = WaitAndFind(locator);
            ((IJavaScriptExecutor)_driver)
                .ExecuteScript("arguments[0].scrollIntoView(true);", element);
        }

        private void ClickAndSelectDropdown(By dropdown, string visibleText)
        {
            var select = new SelectElement(WaitAndFind(dropdown));
            select.SelectByText(visibleText);
        }


        public void OpenShareSkillPage()
        {
            WaitAndFind(ShareSkillButton).Click();
        }

        public void EnterTitle(string title)
        {
            var input = WaitAndFind(TitleInput);
            input.Clear();
            input.SendKeys(title);
        }

        public void EnterDescription(string description)
        {
            var input = WaitAndFind(DescriptionInput);
            input.Clear();
            input.SendKeys(description);
        }

        public void SelectCategory(string category, string subCategory = null)
        {
            ClickAndSelectDropdown(CategoryDropdown, category);

            if (!string.IsNullOrEmpty(subCategory))
            {
                _wait.Until(ExpectedConditions.ElementIsVisible(SubCategoryDropdown));
                ClickAndSelectDropdown(SubCategoryDropdown, subCategory);
            }
        }
        public void EnterTags(params string[] tags)

        {
          
            var tagInput = WaitAndFind(TagInput);

            foreach (var tag in tags)
            {
                tagInput.SendKeys(tag);
                tagInput.SendKeys(Keys.Enter);
                _wait.Until(driver =>
                    driver.FindElements(By.XPath($"//span[text()='{tag}']")).Count > 0
                );
            }

            WaitAndFind(TagsLabel).Click();
        }

        public void SelectServiceType(string type)
        {
            ((IJavaScriptExecutor)_driver)
                .ExecuteScript("window.scrollBy(0, 400);");

            var checkbox = type.Equals("One-off service", StringComparison.OrdinalIgnoreCase)
                ? WaitAndFind(ServiceTypeOneOff)
                : WaitAndFind(ServiceTypeHourly);

            ((IJavaScriptExecutor)_driver)
                .ExecuteScript("arguments[0].classList.add('checked');", checkbox);

            ((IJavaScriptExecutor)_driver)
                .ExecuteScript(
                    "arguments[0].querySelector('input').checked = true;", checkbox);
        }

        public void SelectLocationType(string location)
        {
            ScrollIntoView(LocationOnSite);

            var checkbox = location.Equals("Online", StringComparison.OrdinalIgnoreCase)
                ? WaitAndFind(LocationOnline)
                : WaitAndFind(LocationOnSite);

            ((IJavaScriptExecutor)_driver)
                .ExecuteScript("arguments[0].classList.add('checked');", checkbox);

            ((IJavaScriptExecutor)_driver)
                .ExecuteScript("arguments[0].querySelector('input').checked = true;", checkbox);
        }

         public void UploadWorkSample(string filePath)
         {
             ScrollIntoView(WorkSampleUpload);
             WaitAndFind(WorkSampleUpload).Click();

             var fileInput = _driver.FindElement(By.XPath("//input[@type='file']"));
             fileInput.SendKeys(filePath);
         }

      
        public void SelectActiveStatus(string status)
        {
            
            string radioValue = status.Equals("Active", StringComparison.OrdinalIgnoreCase) ? "true" : "false";

           
            IWebElement radioButton = _driver.FindElement(By.XPath($"//input[@name='isActive' and @value='{radioValue}']"));

            
            ((IJavaScriptExecutor)_driver).ExecuteScript("arguments[0].scrollIntoView({block: 'center'});", radioButton);

            
            ((IJavaScriptExecutor)_driver).ExecuteScript("arguments[0].click();", radioButton);

            
            Thread.Sleep(200);
        }


        public void SaveShareSkill()
         {
             ScrollIntoView(SaveButton);
             WaitAndFind(SaveButton).Click();
         }

         public void CancelShareSkill()
         {
             ScrollIntoView(CancelButton);
             WaitAndFind(CancelButton).Click();
         }
        
        private By SkillTradeExchangeDiv => By.XPath("//input[@name='skillTrades' and @value='true']/parent::div");
        private By SkillTradeCreditDiv => By.XPath("//input[@name='skillTrades' and @value='false']/parent::div");

        private By SkillExchangeInputField => By.XPath("//input[@class='ReactTags__tagInputField']");
        private By CreditInputField => By.XPath("//input[@name='charge']");
       
        public void SelectSkillTrade(string tradeType, string value)
        {
            IWebElement radioButton;

            if (tradeType.Equals("SkillExchange", StringComparison.OrdinalIgnoreCase))
            {
                radioButton = _driver.FindElement(
                    By.XPath("//input[@name='skillTrades' and @value='true']")
                );
            }
            else 
            {
                radioButton = _driver.FindElement(
                    By.XPath("//input[@name='skillTrades' and @value='false']")
                );
            }

    ((IJavaScriptExecutor)_driver)
        .ExecuteScript("arguments[0].scrollIntoView({block:'center'});", radioButton);

            ((IJavaScriptExecutor)_driver)
                .ExecuteScript("arguments[0].click();", radioButton);

            
            if (tradeType.Equals("SkillExchange", StringComparison.OrdinalIgnoreCase))
            {
                var skillInput = _wait.Until(
                    ExpectedConditions.ElementIsVisible(By.CssSelector(".ReactTags__tagInputField"))
                );
                skillInput.SendKeys(value + Keys.Enter);
            }
            else
            {
                var creditInput = _wait.Until(
                    ExpectedConditions.ElementIsVisible(By.Name("charge"))
                );
                creditInput.Clear();
                creditInput.SendKeys(value);
            }
        }
       
        private By CalendarEventSaveButton => By.XPath("//button[contains(text(),'Save') and contains(@class,'k-button')]");
        
        private By CalendarEventRepeatDropdown =>
        By.XPath("//div[@data-role='recurrenceeditor']//span[contains(@class,'k-dropdown-wrap')]");
   public void AddCalendarEvent(
    string title,
    string start,
    string end,
    bool allDay,
    string repeat,
    string description = "",
    string owner = null
)
        {
            
            if (!string.IsNullOrEmpty(title))
                WaitAndFind(CalendarEventTitle).SendKeys(title);

            
            if (!string.IsNullOrEmpty(start))
            {
                var startInput = WaitAndFind(CalendarEventStart);
                startInput.Click();
                startInput.SendKeys(Keys.Control + "a");
                startInput.SendKeys(Keys.Delete);
                startInput.SendKeys(start);
                startInput.SendKeys(Keys.Tab); 
            }

            
            if (!string.IsNullOrEmpty(end))
            {
                var endInput = WaitAndFind(CalendarEventEnd);
                endInput.Click();
                endInput.SendKeys(Keys.Control + "a");
                endInput.SendKeys(Keys.Delete);
                endInput.SendKeys(end);
                endInput.SendKeys(Keys.Tab); 
            }

           
            if (allDay)
            {
                var allDayCheckbox = WaitAndFind(CalendarEventAllDay);
                allDayCheckbox.Click();
                allDayCheckbox.SendKeys(Keys.Tab); 
            }

           
            if (!string.IsNullOrEmpty(repeat))
            {
                SetRecurrenceRule(repeat); 

                
                ((IJavaScriptExecutor)_driver).ExecuteScript(@"
        var elem = document.querySelector('[name=recurrenceRule]');
        if(elem) { elem.blur(); }
    ");

                Thread.Sleep(200); 
            }

            
            if (!string.IsNullOrEmpty(description))
            {
                var desc = WaitAndFind(CalendarEventDescription);
                desc.Clear();
                desc.SendKeys(description);

                
                ((IJavaScriptExecutor)_driver).ExecuteScript("arguments[0].blur();", desc);
            }


            
            if (!string.IsNullOrEmpty(owner))
            {
                var ownerDropdown = WaitAndFind(CalendarEventOwnerDropdown);
                ((IJavaScriptExecutor)_driver).ExecuteScript("arguments[0].scrollIntoView(true);", ownerDropdown);
                ownerDropdown.Click();

                var ownerOption = _wait.Until(driver =>
                    driver.FindElement(By.XPath(
                        $"//ul[contains(@class,'k-list-container')]//li[normalize-space()='{owner}']"
                    ))
                );
                ownerOption.Click();
            }

            
            ScrollIntoView(CalendarEventSaveButton);
            WaitAndFind(CalendarEventSaveButton).Click();
        }

       
        private void SetRecurrenceRule(string repeat)
        {
            ((IJavaScriptExecutor)_driver).ExecuteScript(@"
        var recurrenceInput = document.querySelector('[name=recurrenceRule]');
        if (recurrenceInput) {
            recurrenceInput.value = arguments[0];
            recurrenceInput.dispatchEvent(new Event('change', { bubbles: true }));
        }
    ", repeat);
        }

        private By TitleCharacterCounter =>
    By.XPath("//p[contains(text(),'Characters remaining')]");
        public string GetTitleCharacterRemainingText()
        {
            try
            {
                return WaitAndFind(TitleCharacterCounter).Text.Trim();
            }
            catch (WebDriverTimeoutException)
            {
                return null;
            }
        }
        private By DescriptionCharacterCounter =>
    By.XPath("//textarea[@name='description']/following::p[contains(text(),'Characters remaining')][1]");


        public string GetDescriptionCharacterRemainingText()
        {
            try
            {
                return WaitAndFind(DescriptionCharacterCounter).Text.Trim();
            }
            catch (WebDriverTimeoutException)
            {
                return null;
            }
        }

        private By TitleMandatoryMessage =>
       By.XPath("//div[contains(@class,'ui basic red prompt') and contains(text(),'Title is required')]");
        public string GetTitleMandatoryError()
        {
            try
            {
                return WaitAndFind(TitleMandatoryMessage).Text.Trim();
            }
            catch (WebDriverTimeoutException)
            {
                return null;
            }
        }

        
        private By TitleInlineError => By.XPath("//div[contains(@class,'ui basic red prompt label') and contains(text(),'First character must')]");
       
        public string GetTitleInlineError()
        {
            try
            {
                var errorElement = WaitAndFind(TitleInlineError);
                return errorElement.Text.Trim();
            }
            catch (NoSuchElementException)
            {
                return string.Empty; 
            }
        }

        
        private By DescriptionInlineError => By.XPath("//div[contains(@class,'ui basic red prompt label') and contains(text(),'Special characters are not allowed')]");
       
        public string GetDescriptionInlineError()
        {
            try
            {
                var errorElement = WaitAndFind(DescriptionInlineError);
                return errorElement.Text.Trim();
            }
            catch (NoSuchElementException)
            {
                return string.Empty; 
            }
        }

       
        public string GetInlineErrorForField(string fieldName)
        {
            try
            {
                var errorElement = WaitAndFind(By.XPath($"//div[contains(@class,'ui basic red prompt label') and contains(text(),'{fieldName}')]"));
                return errorElement.Text.Trim();
            }
            catch (NoSuchElementException)
            {
                return string.Empty;
            }
        }

        public string GetToastMessage()
        {
            try
            {
                var toast = _wait.Until(ExpectedConditions.ElementIsVisible(ToastMessageLocator));
                return toast.Text.Trim();
            }
            catch
            {
                return string.Empty;
            }
        }
    }
}


