-- ===== follower: a join table for tracking the many-to-many relationship of users following other users =====
-- blocker_app_user_id: the id of the user doing the blocking
-- blocked_app_user_id: the id of the user being blocked
CREATE TABLE blocking (
    blocker_app_user_id int NOT NULL,
    blocked_app_user_id int NOT NULL,

    CONSTRAINT blocking_primary_key PRIMARY KEY (blocker_app_user_id, blocked_app_user_id),
    CONSTRAINT blocker_app_user_id_foreign_key FOREIGN KEY (blocker_app_user_id) REFERENCES app_user (id),
    CONSTRAINT blocked_app_user_id_foreign_key FOREIGN KEY (blocked_app_user_id) REFERENCES app_user (id)
);