using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ProjectMarsAutomationAdvanceTask.Pages.Components
{
    public class NotificationComponent
    {
        private readonly IWebDriver _driver;
        private readonly WebDriverWait _wait;

        public NotificationComponent(IWebDriver driver)
        {
            _driver = driver;
            _wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
        }

        private By NotificationDropdown =>
            By.XPath("//div[contains(@class,'ui top left pointing dropdown item') and .//text()[contains(.,'Notification')]]");
       
        private By NotificationBadge =>
            By.XPath("//div[contains(@class,'ui top left pointing dropdown item')]//div[contains(@class,'floating ui')]");
       
        private By NotificationMenu =>
            By.XPath("//div[contains(@class,'ui menu') and not(contains(@class,'hidden'))]");

        private By NotificationItems =>
            By.XPath("//div[contains(@class,'ui menu') and not(contains(@class,'hidden'))]//div[contains(@class,'item') and @name]");

        private By FirstNotificationLink =>
            By.XPath("(//div[contains(@class,'ui menu')]//a[contains(@href,'/Home/')])[1]");

        private By NoNotificationsMessage =>
            By.XPath("//*[contains(text(),'You have no notifications')]");

        private By DashboardNotificationCheckboxes =>
            By.XPath("//input[@type='checkbox']");       
        private By DashboardNotifications =>
            By.XPath("//div[contains(@class,'notification')]");      
        private By LoadMoreButton =>
            By.XPath("//a[contains(@class,'ui button') and contains(text(),'Load More')]");
        private By ShowLessButton =>
            By.XPath("//a[contains(@class,'ui button') and contains(text(),'Show Less')]");    
        private By MarkAllAsReadLink =>
            By.XPath("//a[normalize-space()='Mark all as read']");       
        private By SeeAllLink =>
            By.XPath("//a[normalize-space()='See All...']");
        private By SelectAllButton => By.XPath("//div[@data-tooltip='Select all']");
        private By UnselectAllButton => By.XPath("//div[@data-tooltip='Unselect all']");
        private By DeleteSelectionButton => By.XPath("//div[@data-tooltip='Delete selection']");
        private By MarkAsReadButton => By.XPath("//div[@data-tooltip='Mark selection as read']");

        
        private IWebElement WaitAndFind(By locator)
        {
            return _wait.Until(ExpectedConditions.ElementIsVisible(locator));
        }

        
        public void OpenNotificationDropdown()
        {
            WaitAndFind(NotificationDropdown).Click();
            WaitAndFind(NotificationMenu);
        }

        public void ClickMarkAllAsRead()
        {
            OpenNotificationDropdown();
            WaitAndFind(MarkAllAsReadLink).Click();
        }

        public void ClickSeeAll()
        {
            OpenNotificationDropdown();
            WaitAndFind(SeeAllLink).Click();
        }

        public void ClickFirstNotification()
        {
            var notification = WaitAndFind(FirstNotificationLink);
            notification.Click();
        }


        public void ClickNotificationBar()
        {
            WaitAndFind(NotificationDropdown).Click();
        }


        public int GetVisibleDashboardNotificationCount()
        {
            return _driver.FindElements(DashboardNotifications)
                          .Count(e => e.Displayed);
        }

        public void ClickLoadMore()
        {
            var btn = _wait.Until(ExpectedConditions.ElementToBeClickable(LoadMoreButton));
            ((IJavaScriptExecutor)_driver).ExecuteScript("arguments[0].scrollIntoView(true);", btn);
            btn.Click();
        }

        public void ClickShowLess()
        {
            var btn = _wait.Until(ExpectedConditions.ElementToBeClickable(ShowLessButton));
            ((IJavaScriptExecutor)_driver).ExecuteScript("arguments[0].scrollIntoView(true);", btn);
            btn.Click();
        }

        public bool IsShowLessVisible()
        {
            return _driver.FindElements(ShowLessButton).Any(e => e.Displayed);
        }

        public bool IsLoadMoreVisible()
        {
            return _driver.FindElements(LoadMoreButton).Any(e => e.Displayed);
        }

        public int GetNotificationItemCount()
        {
            OpenNotificationDropdown();
            return _driver.FindElements(NotificationItems).Count;
        }

        public bool AreNotificationsPresent()
        {
            OpenNotificationDropdown();
            return _driver.FindElements(NotificationItems).Any();
        }

            
        public void OpenNotificationDashboard()
        {
            OpenNotificationDropdown();
            WaitAndFind(SeeAllLink).Click();
           
            _wait.Until(driver => driver.FindElements(DashboardNotificationCheckboxes).Count > 0);
        }

        public void ClickSelectAll() => WaitAndFind(SelectAllButton).Click();
        public void ClickUnselectAll() => WaitAndFind(UnselectAllButton).Click();
        public void ClickDeleteSelection() => WaitAndFind(DeleteSelectionButton).Click();
        public void ClickMarkAsRead() => WaitAndFind(MarkAsReadButton).Click();

    
        public bool IsDeleteSelectionVisible()
        {
            return _driver.FindElements(DeleteSelectionButton).Any(e => e.Displayed);
        }

        public bool IsMarkAsReadVisible()
        {
            return _driver.FindElements(MarkAsReadButton).Any(e => e.Displayed);
        }

        public bool IsUnselectAllVisible()
        {
            return _driver.FindElements(UnselectAllButton).Any(e => e.Displayed);
        }

        public void ToggleNotificationOnDashboard(int index)
        {
            
            _wait.Until(driver => driver.FindElements(DashboardNotificationCheckboxes).Count > 0);

            var checkboxes = _driver.FindElements(DashboardNotificationCheckboxes);

            if (index >= checkboxes.Count)
                throw new ArgumentOutOfRangeException(nameof(index),
                    $"Requested index {index}, but only {checkboxes.Count} notifications exist.");

            IWebElement checkbox = checkboxes[index];

            
            ((IJavaScriptExecutor)_driver)
                .ExecuteScript("arguments[0].scrollIntoView(true);", checkbox);

            
            checkbox.Click();
        }

        public int GetSelectedCountOnDashboard()
        {
            var checkboxes = _driver.FindElements(DashboardNotificationCheckboxes);
            return checkboxes.Count(cb => cb.Selected);
        }

        public int GetNotificationCount() 
        {
            return _driver.FindElements(DashboardNotificationCheckboxes).Count;
        }

       
        public bool IsNotificationBadgeDisplayed()
        {
            return _driver.FindElements(NotificationBadge).Any();
        }

        public bool IsNotificationDropdownOpened()
        {
            return _wait.Until(driver =>
                driver.FindElements(NotificationMenu).Count > 0
            );
        }

        public bool IsNoNotificationsMessageDisplayed()
        {
            return _driver.FindElements(NoNotificationsMessage).Any();
        }
        public int GetNotificationBadgeCount()
        {
            if (!IsNotificationBadgeDisplayed())
                return 0;

            var text = WaitAndFind(NotificationBadge).Text.Trim();
            return int.TryParse(text, out int count) ? count : 0;
        }
    }
}
