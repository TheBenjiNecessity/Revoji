--Following
CREATE TABLE follower (
	id serial,

    follower_app_user_id int NOT NULL, --the person who is doing the following
    following_app_user_id int NOT NULL, --the person who is being followed

	CONSTRAINT following_primary_key PRIMARY KEY (id),
    CONSTRAINT follower_app_user_id_foreign_key FOREIGN KEY (follower_app_user_id) REFERENCES app_user (id),
    CONSTRAINT following_app_user_id_foreign_key FOREIGN KEY (following_app_user_id) REFERENCES app_user (id)
);