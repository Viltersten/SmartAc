# Theorem Practical Exercise - Priorities for Backend-only Variation

## Backend-Only Scope and Priorities

As a backend developer you will be able to make significant progress on features during this exercise.  In the spec you will notice there are two categories of backend tasks, those prefixed BE-DEV (device API servicing IoT devices) and those with BE-ADM (admin API servicing the Web UI).  There are more tasks here than you have time to complete, so please make as much progress as you can implementing this list as time allows:

1. [BE-DEV-1](smartac-spec.md#be-dev-1) - A device can self-register with the server (_open endpoint, no auth_)
2. [BE-DEV-2](smartac-spec.md#be-dev-2) - A device will continually report its sensor readings to the server (_secure endpoint, requires auth_)
3. [BE-DEV-3](smartac-spec.md#be-dev-3) - Received device data that is out of expected safe ranges should produce alerts (_internal logic_)
4. [BE-ADM-1](smartac-spec.md#be-adm-1) - User Login (_open endpoint, no auth_)
5. [BE-ADM-3](smartac-spec.md#be-adm-3) - List recently registered devices (_secure endpoint, requires auth_)
6. [BE-ADM-4](smartac-spec.md#be-adm-4) - List sensor readings for a device by date range (_secure endpoint, requires auth_)
7. [BE-ADM-5](smartac-spec.md#be-adm-5) - Aggregate sensor readings for a device by date range (_secure endpoint, requires auth_)
8. [BE-ADM-6](smartac-spec.md#be-adm-6) - List alerts active in the system  (_secure endpoint, requires auth_)
9. [BE-ADM-10](smartac-spec.md#be-adm-10) - Search for a device by serial number (_secure endpoint, requires auth_)
10. [BE-DEV-4](smartac-spec.md#be-dev-4) - Device alerts should merge and not duplicate (_internal logic_)
11. [BE-DEV-5](smartac-spec.md#be-dev-5) - Device alerts may self resolve (_internal logic_)
12. [BE-DEV-6](smartac-spec.md#be-dev-6) - Device sensor data that does not validate must be preserved (_internal logic_)
13. [BE-DEV-7](smartac-spec.md#be-dev-7) - Devices sending a lot of invalid data should cause a new alert (_internal logic_)
14. [BE-ADM-11](smartac-spec.md#be-adm-11) - Filter devices by registration date   (_secure endpoint, requires auth_)
15. [BE-ADM-7](smartac-spec.md#be-adm-7) - Alerts can be marked viewed  (_secure endpoint, requires auth_)
16. [BE-ADM-8](smartac-spec.md#be-adm-8) - Alerts can be marked ignored  (_secure endpoint, requires auth_)
17. [BE-ADM-9](smartac-spec.md#be-adm-9) - Alert data can be listed along with sensor readings (_internal logic_)
18. [BE-ADM-2](smartac-spec.md#be-adm-2) - User logout (_secure endpoint, requires auth_)

A good goal is to make it through the first 11 items if possible.
