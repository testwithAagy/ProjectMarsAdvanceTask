Project Mars – Advance Task
===========================

A .NET-based test automation framework for Project Mars, using Selenium WebDriver, NUnit, Page Object Model (POM), JSON-based test data, and Extent Reports.
This framework is designed to automate web application testing with a clean, maintainable, and scalable architecture, aligned with industry best practices.

----Overview----

This framework provides automated functional testing for the Project Mars web application, covering both manual and automation requirements defined


----Key Technologies Used----

Selenium WebDriver – Browser automation and UI interaction

NUnit – Test execution, assertions, fixtures, and hooks

Extent Reports – HTML test execution reports

Page Object Model (POM) – Component-based UI abstraction

JSON Data Handling – Test data externalized using JSON files

C# (.NET) – Automation framework implementation



----Prerequisites----

.NET SDK: Version 8.0 or higher
https://dotnet.microsoft.com

IDE: Visual Studio (recommended) or Visual Studio Code

Browser: Google Chrome

WebDriver: ChromeDriver (version compatible with installed Chrome)


----Overview of My Implementation----

This framework automates Project Mars core features listed Such as , Profile availability, Languages, Skills, Share skill, Search skill, Notifications.

-----Key Highlights-----

Component-driven POM design aligned with UI layout

Manual test cases first, followed by full automation

Positive, negative, valid, invalid, and destructive scenarios

JSON-driven test data for profile updates

Reusable steps and helpers to ensure maintainability

Clear separation of concerns: Tests → Steps → Components → Helpers


----Test Flow----

Each test starts from a known application state

Navigation handled via reusable step methods

UI actions encapsulated in component classes

Assertions placed in test or assertion helper classes

Explicit waits used to ensure UI stability

Tests designed to be independent and repeatable

JSON Data Handling

Test data stored in external JSON files

JSON mapped to strongly-typed model classes

Data read at test level only

Supports:

Positive scenarios

Extended positive cases

Negative valid invalid & destructive cases


Reporting:

Extent Reports integrated

HTML report generated after execution



-----Running the Tests-----

Restore dependencies

dotnet restore


Build the solution

dotnet build


Execute tests

dotnet test


Open the generated Extent Report to view execution results


--- Bugs / Non testable components Identified During Testing

Profile – Location not editable
• Test Case: UpdateLocationShouldBeAllowedForUser("Wellington")
• Actual Behavior: User cannot edit location; “No Access” shown.
• Expected Behavior: User should be able to update location successfully.

Profile – Availability cannot be left blank
• Test Case: SaveProfileWithoutAvailabilityShouldShowValidationError()
• Actual Behavior: No option provided to leave Availability blank.
• Expected Behavior: System should allow selection and validate if left empty.

Language – Binary / non-printable characters accepted
• Test Case: AddLanguageWithBinaryCharactersShouldShowInvalidInputError("\x00\x01\x02")
• Actual Behavior: Binary/control characters are accepted as normal input; no error message shown.
• Expected Behavior: Input should be sanitized or rejected and an invalid input error message should be displayed.

Language – SQL Injection input accepted
• Test Case: AddLanguageWithSqlInjectionPayloadShouldBeRejected("'; DROP TABLE Users;--")
• Actual Behavior: SQL payload is accepted without any validation error.
• Expected Behavior: System should sanitize or reject SQL injection attempts and display an invalid input error message.

Language – Malicious HTML/XSS executed
• Test Case: AddLanguageWithHtmlImageTagShouldNotTriggerScript("<img src=x onerror=alert('Hacked')>")
• Actual Behavior: Alert popup is triggered.
• Expected Behavior: Input should be sanitized or rejected; script should not execute and an invalid input message should be shown.

Language – Invalid special characters accepted
• Test Case: EditLanguageWithSpecialCharactersShouldShowValidationError("@@##$$")
• Actual Behavior: Invalid characters are accepted.
• Expected Behavior: System should display “Invalid characters not allowed”.

Language – Numeric value accepted in text field
• Test Case: EditLanguageWithNumericValueShouldShowValidationError("123456")
• Actual Behavior: Numeric value is accepted as valid input.
• Expected Behavior: System should display “Enter valid text only”.

Skill – Duplicate (uppercase) accepted
• Test Case: AddDuplicateSkillRecordWithDifferentCaseShouldShowErrorMessage("JAVA")
• Actual Behavior: Duplicate skill is accepted.
• Expected Behavior: System should prevent duplicate entries regardless of case.

Skill – Numeric value accepted
• Test Case: AddSkillWithOnlyNumbersShouldShowInvalidSkillNameError("232435")
• Actual Behavior: Numeric skill name is accepted.
• Expected Behavior: System should display “Invalid Skill name”.

Skill – Special characters accepted
• Test Case: AddSkillWithSpecialCharactersShouldShowInvalidSkillNameError("%$%^$^%")
• Actual Behavior: Skill name with special characters is accepted.
• Expected Behavior: System should display “Invalid Skill name”.

Skill – Exceeds maximum length accepted
• Test Case: AddSkillExceedingMaxLengthShouldShowValidationError("10000+ chars")
• Actual Behavior: Over-length skill input is accepted.
• Expected Behavior: System should display “Input exceeds maximum length allowed”.

Skill – Binary / non-printable characters accepted
• Test Case: AddSkillWithBinaryCharactersShouldShowInvalidInputError("\x00\x01\x02")
• Actual Behavior: Binary characters are accepted; no error message shown.
• Expected Behavior: Input should be sanitized or rejected with an invalid input error.

Skill – SQL Injection input accepted
• Test Case: AddSkillWithSqlInjectionPayloadShouldBeRejected("'; DROP TABLE Users;--")
• Actual Behavior: SQL injection input is accepted.
• Expected Behavior: System should sanitize or reject the input and show an error message.

Skill – Malicious HTML/XSS executed
• Test Case: AddSkillWithHtmlImageTagShouldNotTriggerScript("<img src=x onerror=alert('Hacked')>")
• Actual Behavior: Alert popup is triggered.
• Expected Behavior: Script execution should be prevented and invalid input error displayed.

Skill – Invalid characters accepted during edit
• Test Case: EditSkillWithSpecialCharactersShouldShowValidationError("@@##$$")
• Actual Behavior: Invalid characters are accepted.
• Expected Behavior: System should display “Invalid characters not allowed”.

Skill – Numeric value accepted during edit
• Test Case: EditSkillWithNumericValueShouldShowValidationError("123456")
• Actual Behavior: Numeric value is accepted.
• Expected Behavior: System should display “Enter valid text only”.

Share Skill – Event not saved
• Test Case: CreateCalendarEventWithValidDetailsShouldBeSaved()
• Actual Behavior: Event is not saved or displayed.
• Expected Behavior: Event should be saved and displayed on the calendar.

Share Skill – Credit service charge greater than 10 not accepted
• Test Case: AddShareSkillWithCreditServiceChargeGreaterThanTenShouldBeAccepted(12)
• Actual Behavior: Service charge value is not accepted.
• Expected Behavior: Credit value greater than 10 should be accepted.

Share Skill – Random numeric characters validation
• Test Case: AddRandomNumericValuesToTextFieldsShouldShowValidationError("3.45466E+12")
• Actual Behavior: Error message shown and save prevented.
• Expected Behavior: System should show error message and prevent saving.

Search – Leading and trailing spaces not trimmed
• Test Case: SearchWithLeadingAndTrailingSpacesShouldTrimInput(" Au tomation ")
• Actual Behavior: Spaces are not trimmed; no results shown.
• Expected Behavior: Input should be trimmed and relevant results displayed.

Search – Special characters return all results
• Test Case: SearchWithOnlySpecialCharactersShouldShowNoResults("&^&%$%$")
• Actual Behavior: All results are displayed.
• Expected Behavior: System should show “No results found”.

----Conclusion----

This task delivers a complete manual and automated testing framework for Project Mars, covering core features like profile management, notifications, share skill, search skill, and sign-in flows. The component-driven POM design, JSON-driven test data, and robust positive, negative, and edge-case coverage ensure maintainable, reliable, and repeatable tests.
