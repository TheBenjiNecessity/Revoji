\copy app_user from './sample_users.csv' delimiter ',' csv header;
\copy reviewable from './sample_reviewables.csv' delimiter ',' csv header;
\copy review from './sample_reviews.csv' delimiter ',' csv header;
\copy follower from './sample_followers.csv' delimiter ',' csv header;
\copy review_like from './sample_likes.csv' delimiter ',' csv header;

-- init every app_user to have a hashed password of 'password' with same salt
--update app_user set password = '$2b$10$InGuymmSQhNObJbmo0lx/upbu4JFPsPYgmfnugQu71baAlZCXedDi';
--update app_user set salt = '$2b$10$InGuymmSQhNObJbmo0lx/u';

-- If you want to completely obliterate your database and start from scratch
-- TRUNCATE blocking, review_reply, review_like, follower, review, reviewable, app_user RESTART IDENTITY;