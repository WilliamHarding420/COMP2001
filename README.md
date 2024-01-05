# COMP2001
This is the git repository for the profile microservice I have implemented for the COMP2001 module 70% coursework.

# Purpose
This microservice aims to provide the necessary endpoints for authenticating a user, allowing them to view their own details, update their details, add favourite activities and delete favourite activities.

It also has some extra functions for admins, such as creating new activities, deleting activities and deleting user accounts.

# Endpoint Overview
All of the following endpoints only accept and output data in the application/json format, and all follow the same return data layout:
{
	"Success": true/false,
	"Message": "The return message, sometimes a string, list or another JSON object"
}

## Endpoints
### Get All Activities
HTTP Method: Get  
Endpoint: /activities   
Parameters: None   

Example Output   
{
	"Success": true,
	"Message": [
		{ "ActivityID":1, "Activity": "Running" }
	]
}

### Authorize User
HTTP Method: Post   
Endpoint: /user/auth
Parameters: Email, Password   

On success, this endpoint returns an authorization token, referred to as Token in endpoint parameters, and is associated with your ID, thus must be sent for any admin actions or account actions.   

Example Output   
{
	"Success": true,
	"Message": "authorization token"
}

### Delete Activity
HTTP Method: Delete   
Endpoint: /activity/delete   
Parameters: Token, Activity ID   

Example Output   
{
	"Success": true,
	"Message": "Activity Deleted."
}

### Delete User
HTTP Method: Delete   
Endpoint: /user/delete/{id}   
Parameters: Token, User ID (Parameter in route)   
Route Example: /user/delete/1   

Example Output   
{
	"Success": true,
	"Message": "User ID {id} deleted successfully."
}

### Get Activity
HTTP Method: Get   
Endpoint: /activity/{id}   
Parameters: ID (Parameter in route)   
Route Example: /activity/1   

Example Output   
{
	"Success": true,
	"Message": { 
		"ActivityID": 1,
		"Activity": "Running" 
	}
}

### Get Private Details
HTTP Method: Post   
Endpoint: /user/private   
Parameters: Token   

Example Output   
{
	"Success": true,
	"Message": {
		"Username": "Ada Lovelace",
		"Email": "ada@plymouth.ac.uk",
		"AboutMe": null,
		"Location": null,
		"Units": "Metric",
		"ActivityTimePreference": "Pace",
		"Height": null,
		"Weight": null,
		"Birthday": null,
		"ProfilePictureLink": null,
		"Language": "English (UK)"
	}
}

### Get Favourite Activities
HTTP Method: Post   
Endpoint: /user/activities   
Parameters: Token   

Example Output
{
	"Success": true,
	"Message": [
		{"UserID": 1, "Username": "Ada Lovelace", "ActivityID": 1, "Activity": "Running"},
		{"UserID": 1, "Username": "Ada Lovelace", "ActivityID": 3, "Activity": "Jogging"}
	]
}

### New Activity
HTTP Method: Put   
Endpoint: /activity/new   
Parameters: Token, Activity   

Example Output
{
	"Success": true,
	"Message": "Activity Successfully Added."
}

### New Profile
HTTP Method: Put   
Endpoint: /user/new   
Parameters: Username, Email, Password   

Example Output
{
	"Success": true,
	"Message": "User successfully created."
}

### Public Profile
HTTP Method: Get   
Endpoint: /user/public/{id}   
Parameters: User ID (Parameter in route)
Route Example: /user/public/1   

Example Output
{
	"Success": true,
	"Message": {
		"Username": "Ada Lovelace",
		"AboutMe": null,
		"ProfilePictureLink": null
	}
}

### Update Activity
HTTP Method: Post   
Endpoint: /activity/update/{id}   
Parameters: Token, Activity, Activity ID (Parameter in route)   
Route Example: /activity/update/1   

Example Output
{
	"Success": true,
	"Message": "Activity Name Updated."
}

### Update Details
HTTP Method: Post   
Endpoint: /user/update   
Parameters: Token
Optional Parameters: 
 - Username
 - Email
 - AboutMe
 - Location
 - Units
 - ActivityTimePreference
 - Height
 - Weight
 - Birthday
 - Language

Example Output
{
	"Success": true,
	"Message": "Details updated."
}

### User Favourite Activity
HTTP Method: Put   
Endpoint: /user/activity/add   
Parameters: Token, Activity ID   

Example Output
{
	"Success": true,
	"Message": "Favourite activity added."
}

### User Delete Favourite Activity
HTTP Method: Delete   
Endpoint: /user/activity/delete   
Parameters: Token, Activity ID   

Example Output
{
	"Success": true,
	"Message": "Favourite activity deleted."
}



