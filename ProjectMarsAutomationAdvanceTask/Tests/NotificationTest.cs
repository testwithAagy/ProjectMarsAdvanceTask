using NUnit.Framework;
using OpenQA.Selenium;
using ProjectMarsAutomationAdvanceTask.Steps;
using ProjectMarsAutomationAdvanceTask.Tests.Base;
using System.Threading;

namespace ProjectMarsAutomationAdvanceTask.Tests
{
    [TestFixture]
    public class NotificationTests : BaseTest
    {
        private NotificationSteps _notificationSteps;

        [SetUp]
        public void TestSetup()
        {
            _notificationSteps = new NotificationSteps(Driver);
        }

        

        [Test]
        public void Notification_Badge_ShouldBeVisible_WithValidCount()
        {
            bool isBadgeDisplayed = _notificationSteps.IsNotificationBadgeDisplayed();
            int badgeCount = _notificationSteps.GetNotificationCountFromBadge();

            Assert.IsTrue(isBadgeDisplayed, "Notification badge is not displayed.");
            Assert.Greater(badgeCount, 0, "Notification badge count should be greater than zero.");
        }

        [Test]
        public void Notification_Click_ShouldOpenDropdownAndShowItems()
        {
            _notificationSteps.OpenNotificationDropdown();
            bool isOpened = _notificationSteps.IsNotificationDropdownOpened();
            int notificationCount = _notificationSteps.GetNotificationItemCount();

            Assert.IsTrue(isOpened, "Notification dropdown did not open.");
            Assert.Greater(notificationCount, 0, "No notifications were displayed in the dropdown.");
        }


        [Test]
        public void Notification_MarkAllAsRead_ShouldClearNotifications()
        {
            _notificationSteps.OpenNotificationDropdown();
            int initialCount = _notificationSteps.GetNotificationCountFromBadge();
            Assert.That(initialCount, Is.GreaterThan(0), "No notifications available to mark as read.");

            _notificationSteps.MarkAllNotificationsAsRead();
            Thread.Sleep(1000);
            int updatedCount = _notificationSteps.GetNotificationCountFromBadge();

            Assert.That(updatedCount, Is.EqualTo(0), "Notifications were not cleared after clicking 'Mark all as read'.");
        }

        [Test]
        public void Notification_ClickingNotification_ShouldNavigateToDetailsPage()
        {
            string currentUrl = Driver.Url;
            _notificationSteps.OpenNotificationDropdown();
            _notificationSteps.ClickFirstNotification();

            Assert.That(Driver.Url, Does.Not.EqualTo(currentUrl), "Clicking notification did not change the page.");
            Assert.That(Driver.Url, Does.Contain("ReceivedRequest"), "Notification did not navigate to the expected page.");
        }

        [Test]
        public void Notification_ClickSeeAll_ShouldNavigateToDashboard()
        {
            string currentUrl = Driver.Url;
            _notificationSteps.OpenNotificationDropdown();
            _notificationSteps.ClickSeeAll();

            Assert.That(Driver.Url, Does.Not.EqualTo(currentUrl), "Clicking 'See All' did not change the page.");
            Assert.That(Driver.Url, Does.Contain("Dashboard"), "'See All' link did not navigate to Dashboard page.");
        }

       

        [Test]
        public void Notification_SelectAll_OnDashboard_ShouldSelectAll()
        {
            _notificationSteps.OpenNotificationDashboard();
            _notificationSteps.SelectAllNotifications();

            Assert.AreEqual(
                _notificationSteps.GetDashboardNotificationCount(),
                _notificationSteps.GetSelectedCountOnDashboard(),
                "Select All did not select all notifications."
            );
        }

        [Test]
        public void Notification_UnselectAll_OnDashboard_ShouldDeselectAll()
        {
            _notificationSteps.OpenNotificationDashboard();
            _notificationSteps.SelectAllNotifications();
            Assert.IsTrue(_notificationSteps.GetSelectedCountOnDashboard() > 0, "Precondition failed: No notifications are selected.");

            _notificationSteps.UnselectAllNotifications();
            Assert.AreEqual(0, _notificationSteps.GetSelectedCountOnDashboard(), "Unselect All did not deselect all notifications.");
        }

        [Test]
        public void Notification_MarkSelectedAsRead_OnDashboard_ShouldLoseUnreadState()
        {
            _notificationSteps.OpenNotificationDashboard();
            int totalNotifications = _notificationSteps.GetDashboardNotificationCount();
            Assert.IsTrue(totalNotifications > 0, "Precondition failed: No notifications present.");

            _notificationSteps.SelectAllNotifications();
            int selectedCount = _notificationSteps.GetSelectedCountOnDashboard();
            Assert.IsTrue(selectedCount > 0, "Precondition failed: No notifications were selected.");

            _notificationSteps.MarkSelectedNotificationsAsRead();
            Thread.Sleep(1000);

            int unreadCountAfter = _notificationSteps.GetSelectedCountOnDashboard();
            Assert.AreEqual(0, unreadCountAfter, "Some notifications are still marked as unread after marking as read.");
        }

        [Test]
        public void Notification_ToggleSingleNotification_OnDashboard_ShouldSelectAndUnselect()
        {
            _notificationSteps.OpenNotificationDashboard();
            int totalNotifications = _notificationSteps.GetDashboardNotificationCount();
            Assert.IsTrue(totalNotifications > 0, "Precondition failed: No notifications present.");

            
            _notificationSteps.ToggleNotificationOnDashboard(0);
            int selectedCountAfterSelect = _notificationSteps.GetSelectedCountOnDashboard();
            Assert.AreEqual(1, selectedCountAfterSelect, "First notification was not selected properly.");

            
            _notificationSteps.ToggleNotificationOnDashboard(0);
            int selectedCountAfterUnselect = _notificationSteps.GetSelectedCountOnDashboard();
            Assert.AreEqual(0, selectedCountAfterUnselect, "First notification was not unselected properly.");
        }

        [Test]
        public void Notification_DeleteSelected_OnDashboard_ShouldRemoveNotifications()
        {
            _notificationSteps.OpenNotificationDashboard();
            int initialCount = _notificationSteps.GetDashboardNotificationCount();
            Assert.IsTrue(initialCount > 0, "Precondition failed: No notifications present to delete.");

            _notificationSteps.SelectAllNotifications();
            _notificationSteps.DeleteSelectedNotifications();
            Thread.Sleep(1000);

            int finalCount = _notificationSteps.GetDashboardNotificationCount();
            Assert.Less(finalCount, initialCount, "Notifications were not deleted properly.");
        }


        [Test]
        public void Notification_OpenDropdown_WithNoNotifications_ShouldShowEmptyState()
        {
            
            int badgeCount = _notificationSteps.GetNotificationCountFromBadge();
            Assert.That(badgeCount, Is.EqualTo(0),
                "Precondition failed: Notification badge count is not zero.");           
            _notificationSteps.ClickNotificationBar();
            
            Assert.IsTrue(
                _notificationSteps.IsNoNotificationsMessageDisplayed(),
                "Empty notification message was not displayed."
            );
        }

        [Test]
        public void Notification_DeleteWithoutSelection_ShouldNotBeVisible()
        {
           
            _notificationSteps.OpenNotificationDashboard();
          
            Assert.IsFalse(
                _notificationSteps.IsDeleteSelectionVisible(),
                "Delete Selection button should not be visible when no notifications are selected."
            );
        }

        [Test]
        public void Notification_MarkAsReadWithoutSelection_ShouldNotBeVisible()
        {
            
            _notificationSteps.OpenNotificationDashboard();
           
            Assert.IsFalse(
                _notificationSteps.IsMarkAsReadVisible(),
                "Mark As Read button should not be visible when no notifications are selected."
            );
        }

        [Test]
        public void Notification_UnselectWithoutSelection_ShouldNotBeVisible()
        {
            
            _notificationSteps.OpenNotificationDashboard();
           
            Assert.IsFalse(
                _notificationSteps.IsUnselectAllVisible(),
                "Unselect All button should not be visible when no notifications are selected."
            );
        }

        [Test]
        public void Notification_ShowLess_OnDashboard_ShouldWork()
        {
            
            _notificationSteps.OpenNotificationDashboard();
            
            ((IJavaScriptExecutor)Driver).ExecuteScript("window.scrollBy(0, 300);");           
            if (_notificationSteps.IsLoadMoreVisible())
            {
                _notificationSteps.ClickLoadMoreOnDashboard();
            }          
            Assert.IsTrue(_notificationSteps.IsShowLessVisible(), "'Show Less' button did not appear after Load More.");          
            ((IJavaScriptExecutor)Driver).ExecuteScript("window.scrollBy(0, 300);");           
            _notificationSteps.ClickShowLessOnDashboard();           
            Assert.IsFalse(_notificationSteps.IsShowLessVisible(), "'Show Less' button still visible after collapsing notifications.");
        }


    }
}
