-- ===== like: a one to many join table tracking likes for reviews =====
-- type:        the type of like (like/dislike/meh)
-- review_id:   the review that the like is attached to
-- app_user_id: the user making the review
CREATE TABLE review_like (
    type text NOT NULL,
    created timestamp NOT NULL,
    review_id int NOT NULL,
    app_user_id int NOT NULL,

    CONSTRAINT like_primary_key PRIMARY KEY (review_id, app_user_id),
    CONSTRAINT review_like_foreign_key FOREIGN KEY (review_id) REFERENCES review (id),
    CONSTRAINT review_user_foreign_key FOREIGN KEY (app_user_id) REFERENCES app_user (id)
);