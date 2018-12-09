# Restful Endpoints (url/service-api...)

### App User:

| item            | method | url                                | description                            | authorization |
|-----------------|--------|------------------------------------|----------------------------------------|---------------|
| ApiUser         | Get    | ```/appuser```                     | Get the user model for the 'main' user | Yes           |
| Create          | Post   | ```/appuser```                     | Create a new user                      | No            |
| Read            | Get    | ```/appuser/{id}```                | Get a user by id                       | Yes           |
| Read (handle)   | Get    | ```/appuser/handle/{handle}```     | Get a user by their handle             | Yes           |
| Update          | Put    | ```/appuser/{id}```                | Update a user by id                    | Yes           |
| Delete          | Delete | ```/appuser/{id}```                | Remove a user by id                    | Yes           |
| Stats           | Get    | ```/appuser/{id}/counts```         | Get statistics about the user by id    | Yes           |
| Search          | Get    | ```/appuser/search/{text}```       | Search for app users by given text     | Yes           |
| Followers       | Get    | ```/appuser/{id}/followers```      | List followers for the given user      | Yes           |
| Followings      | Get    | ```/appuser/{id}/followings```     | List followings for the given user     | Yes           |
| Change Password | Put    | ```/appuser/changepassword```      | Change the password for the api user   | Yes           |
| Add Following   | Post   | ```/appuser/{id}/following```      | Add a following for the given user     | Yes           |
| Del Following   | Delete | ```/appuser/{id}/following/{id}``` | Delete a following for the given user  | Yes           |

- - -

### Reviewable:

| item        | method | url                                                           | description                            | authorization |
|-------------|--------|---------------------------------------------------------------|----------------------------------------|---------------|
| Read        | Get    | ```/reviewable/{tpId}?tpName=```                              | Get a reviewable by tpid and tpname    | Yes           |
| Search      | Get    | ```/reviewable/{text}?tpName=&order=&pageStart=&pageLimit=``` | List reviewables by search text        | Yes           |
| List        | Get    | ```/reviewable/type/{type}?order=&pageStart=&pageLimit=```    | List reviewables by type               | Yes           |
| Emoji Stats | Get    | ```/reviewable/stats/icon/{tpId}?tpName=```                   | Get emoji stats                        | Yes           |
| Word Stats  | Get    | ```/reviewable/stats/word/{tpId}?tpName=```                   | Get word stats                         | Yes           |

- - -

### Review:

| item               | method | url                                       | description                            | authorization |
|--------------------|--------|-------------------------------------------|----------------------------------------|---------------|
| Create             | Post   | ```/review```                             | Create a new review                    | Yes           |
| Read               | Get    | ```/review/{id}```                        | Get a review by id                     | Yes           |
| Update             | Put    | ```/review/{id}```                        | Update a review by id                  | Yes           |
| Delete             | Delete | ```/review/{id}```                        | Remove a review by id                  | Yes           |
| List by User       | Get    | ```/review/user/{id}```                   | List reviews from a user               | Yes           |
| List by Reviewable | Get    | ```/review/reviewable/{id}```             | List reviews for a reviewable          | Yes           |
| List by Followings | Get    | ```/review/following/{id}```              | List reviews from a user's followings  | Yes           |
| Create Like        | Post   | ```/review/like```                        | Add a like to a review                 | Yes           |
| Delete Like        | Delete | ```/review/like/{id}```                   | Delete a like from review              | Yes           |
| Create Reply       | Post   | ```/review/reply```                       | Create reply for a review              | Yes           |
| Delete Reply       | Delete | ```/review/reply/{reviewId}?appUserId=``` | Delete reply for a review              | Yes           |
| List Replies       | Get    | ```/review/reply/{reviewId}```            | List replies for a review              | Yes           |

- - -

## Database

- text describing copying data from csv