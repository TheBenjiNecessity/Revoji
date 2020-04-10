-- ===== user_notification: a notification for a user =====
CREATE TABLE user_notification (
	id serial,
	created timestamp,
    notification_data json,

    app_user_id int NOT NULL,

	CONSTRAINT user_notification_primary_key PRIMARY KEY (id),
    CONSTRAINT user_notification_app_user_foreign_key FOREIGN KEY (app_user_id) REFERENCES app_user (id)
);