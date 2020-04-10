-- ===== bookmark: a user created bookmark for a review =====
CREATE TABLE bookmark (
    id serial,
    created timestamp,
    bookmark_data json,

    app_user_id int NOT NULL,
    review_id int NOT NULL,

    CONSTRAINT bookmark_primary_key PRIMARY KEY (id),
    CONSTRAINT bookmark_app_user_foreign_key FOREIGN KEY (app_user_id) REFERENCES app_user (id),
    CONSTRAINT bookmark_review_foreign_key FOREIGN KEY (review_id) REFERENCES review (id)
);