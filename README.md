# DentalOffice
## Controllers:

There are three controllers, Client, Doctor and Registration
 
### ClientController:

Has access for the User role, as well as some endpoints allow you to not log in to the account to execute (e.g. get reserved dates).  You can view doctor information and learn about available procedures

### DoctorController:

The Doctor or Admin role is required to access this controller. Doctors can add and update procedures, also procedures can be archived, in this case the User role will not see this procedure in the list. The doctor, in turn, can also see archived.
Doctors can also view a list of patients, or information about a specific patient. A dentist can view all of his or his appointments. 
```
  /api/Doctor/get-appointments-by-doctor-id/{id}
```
In this case, the id of the doctor who is currently in the account is expected to come from the front, although I haven't restricted the ability to know the appointments of another doctor in any way

### RegistrationController:

This controller allows an administrator or doctor to directly create an account for a patient. As well as assign an account to a new doctor and give the doctor a role.

## Identity

I used Identity to manage user accounts as well as their roles. 

https://localhost:44336/Identity/Account/Login 

When the database is empty and there is no user, a demouser is created by default with a standard password and the Admin role.

### Roles 

I decided to add not 2 but 3 roles, all because users are User, doctors are Doctor, and the owner or administration is allowed to have Admin.

## PS 

* Returned values from endpoint:
```
/api/Client/get-reserved-dates
```
are reserved dates, I think that from the design side should be implemented the design of free dates based on this answer

* Registration doesn't work because of added mandatory fields, but I didn't change that in the default registration, so you have to register via swagger

* Only smtp remains unrealized 
