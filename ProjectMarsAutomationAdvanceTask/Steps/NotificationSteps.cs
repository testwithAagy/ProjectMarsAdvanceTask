using OpenQA.Selenium;
using ProjectMarsAutomationAdvanceTask.Pages.Components;

namespace ProjectMarsAutomationAdvanceTask.Steps
{
    public class NotificationSteps
    {
        private readonly NotificationComponent _notificationComponent;

        public NotificationSteps(IWebDriver driver)
        {
            _notificationComponent = new NotificationComponent(driver);
        }

       
        public void OpenNotificationDropdown() => _notificationComponent.OpenNotificationDropdown();
        public void MarkAllNotificationsAsRead() => _notificationComponent.ClickMarkAllAsRead();
        public void ClickFirstNotification() => _notificationComponent.ClickFirstNotification();
        public void ClickSeeAll() => _notificationComponent.ClickSeeAll();
        public int GetNotificationItemCount() => _notificationComponent.GetNotificationItemCount();
        public bool AreNotificationsPresent() => _notificationComponent.AreNotificationsPresent();
        public bool IsNotificationBadgeDisplayed() => _notificationComponent.IsNotificationBadgeDisplayed();
        public int GetNotificationCountFromBadge() => _notificationComponent.GetNotificationBadgeCount();
        public bool IsNotificationDropdownOpened() => _notificationComponent.IsNotificationDropdownOpened();
        public void OpenNotificationDashboard() => _notificationComponent.OpenNotificationDashboard();
        public void SelectAllNotifications() => _notificationComponent.ClickSelectAll();
        public void UnselectAllNotifications() => _notificationComponent.ClickUnselectAll();
        public void DeleteSelectedNotifications() => _notificationComponent.ClickDeleteSelection();
        public void MarkSelectedNotificationsAsRead() => _notificationComponent.ClickMarkAsRead();

        public void ToggleNotificationOnDashboard(int index = 0) => _notificationComponent.ToggleNotificationOnDashboard(index);

        public bool IsNoNotificationsMessageDisplayed() => _notificationComponent.IsNoNotificationsMessageDisplayed();

        public void ClickNotificationBar() => _notificationComponent.ClickNotificationBar();

        public bool IsDeleteSelectionVisible() => _notificationComponent.IsDeleteSelectionVisible();

        public bool IsMarkAsReadVisible() => _notificationComponent.IsMarkAsReadVisible();

        public bool IsUnselectAllVisible() => _notificationComponent.IsUnselectAllVisible();

        public int GetVisibleNotificationCount() => _notificationComponent.GetVisibleDashboardNotificationCount();

        public void ClickLoadMoreOnDashboard() => _notificationComponent.ClickLoadMore();
        public void ClickShowLessOnDashboard() => _notificationComponent.ClickShowLess();

        public bool IsLoadMoreVisible() => _notificationComponent.IsLoadMoreVisible();
        public bool IsShowLessVisible() => _notificationComponent.IsShowLessVisible();

        public int GetSelectedCountOnDashboard() => _notificationComponent.GetSelectedCountOnDashboard();
        public int GetDashboardNotificationCount() => _notificationComponent.GetNotificationCount();
    }
}

