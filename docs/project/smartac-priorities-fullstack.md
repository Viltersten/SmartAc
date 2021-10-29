# Theorem Practical Exercise - Priorities for Fullstack Variation

## Fullstack Scope and Priorities

As a fullstack developer you have your work cut out for you.  You need to create the BE-DEV api's for a device to register and send data, but also the BE-ADM API's and the UI that uses those API's.  Your priotizations include a lot of tasks and it is unlikely you'll get through more than half of these tasks, so please just implement them in priority order for what time allows:

1. [BE-DEV-1](smartac-spec.md#be-dev-1) - A device can self-register with the server (_open endpoint, no auth_)
2. [BE-DEV-2](smartac-spec.md#be-dev-2) - A device will continually report its sensor readings to the server (_secure endpoint, requires auth_)
3. [BE-DEV-3](smartac-spec.md#be-dev-3) - Received device data that is out of expected safe ranges should produce alerts (_internal logic_)
4. [BE-ADM-1](smartac-spec.md#be-adm-1) - User Login (_open endpoint, no auth_)
5. [BE-ADM-3](smartac-spec.md#be-adm-3) - List recently registered devices (_secure endpoint, requires auth_)
6. [BE-ADM-4](smartac-spec.md#be-adm-4) - List sensor readings for a device by date range (_secure endpoint, requires auth_)
7. [BE-ADM-5](smartac-spec.md#be-adm-5) - Aggregate sensor readings for a device by date range (_secure endpoint, requires auth_)
8. [BE-ADM-6](smartac-spec.md#be-adm-6) - List alerts active in the system  (_secure endpoint, requires auth_)
9. [FE-ADM-1](smartac-spec.md#fe-adm-1) - Login for admin web portal (_using BE-ADM-1_)
10. [FE-ADM-2](smartac-spec.md#fe-adm-2) - Show list of recently registered devices (_using BE-ADM-3_)
11. [FE-ADM-3](smartac-spec.md#fe-adm-3) - Show sensor readings for selected device (_click from FE-ADM-2 list, from BE-ADM-4_)
12. [FE-ADM-4](smartac-spec.md#fe-adm-4) - Show aggregate sensor readings on graph for selected device (_same view as WEB-3, from BE-ADM-5_)
13. [FE-ADM-5](smartac-spec.md#fe-adm-5) - Show un-viewed / un-resolved alerts at the top of all pages or from a notification badge (_from BE-ADM-6_) which click through to see device details (_FE-ADM-3/FE-ADM -4_)
14. [BE-ADM-10](smartac-spec.md#be-adm-10) - Search for a device by serial number (_secure endpoint, requires auth_)
15. [FE-ADM-6](smartac-spec.md#fe-adm-6) - In device list (_FE-ADM-2_) allow searching for device by serial number (_using BE-ADM-10_)
16. [BE-DEV-4](smartac-spec.md#be-dev-4) - Device alerts should merge and not duplicate (_internal logic_)
17. [BE-DEV-5](smartac-spec.md#be-dev-5) - Device alerts may self resolve (_internal logic_)
18. [BE-DEV-6](smartac-spec.md#be-dev-6) - Device sensor data that does not validate must be preserved (_internal logic_)
19. [BE-DEV-7](smartac-spec.md#be-dev-7) - Devices sending a lot of invalid data should cause a new alert (_internal logic_)
20. [BE-ADM-11](smartac-spec.md#be-adm-11) - Filter devices by registration date   (_secure endpoint, requires auth_)
21. [FE-ADM-7](smartac-spec.md#fe-adm-7) - In device list (_FE-ADM-2_) allow filtering devices by registration date range (_using BE-ADM-11_)
22. [BE-ADM-7](smartac-spec.md#be-adm-7) - Alerts can be marked viewed  (_secure endpoint, requires auth_)
23. [BE-ADM-8](smartac-spec.md#be-adm-8) - Alerts can be marked ignored  (_secure endpoint, requires auth_)
24. [FE-ADM-8](smartac-spec.md#fe-adm-8) - When viewing an alert, it may be marked viewed by the user (_using BE-ADM-7_)
25. [FE-ADM-9](smartac-spec.md#fe-adm-9) - When viewing an alert, it may be marked as ignored by the user (_using BE-ADM-8_)
26. [BE-ADM-9](smartac-spec.md#be-adm-9) - Alert data can be listed along with sensor readings (_internal logic_)
27. [FE-ADM-10](smartac-spec.md#fe-adm-10) - When viewing device sensor list (_FE-ADM-3_), annotate the device readings with red color if they were involved in an alert (_using the BE-ADM-9 change to BE-ADM-4_)
28. [BE-ADM-2](smartac-spec.md#be-adm-2) - User logout (_secure endpoint, requires auth_)
29. [FE-ADM-11](smartac-spec.md#fe-adm-11) - A user may logout (_using BE-ADM-2_)

A good goal is to make it through the first 13 items if possible.
