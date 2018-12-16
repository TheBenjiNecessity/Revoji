-- ===== follower: a join table for tracking the many-to-many relationship of users following other users =====
--               created: the timestamp that the user started following the other user
--  follower_app_user_id: the id of the user doing the following
-- following_app_user_id: the id of the user being followed
CREATE TABLE follower (
    created timestamp default now(),

    follower_app_user_id int NOT NULL,
    following_app_user_id int NOT NULL,

    CONSTRAINT following_primary_key PRIMARY KEY (follower_app_user_id, following_app_user_id),
    CONSTRAINT follower_app_user_id_foreign_key FOREIGN KEY (follower_app_user_id) REFERENCES app_user (id),
    CONSTRAINT following_app_user_id_foreign_key FOREIGN KEY (following_app_user_id) REFERENCES app_user (id)
);