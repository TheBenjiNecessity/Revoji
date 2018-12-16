-- ===== review: a many to many join table tracking users who have reviewed reviewables =====
--   title: the title of the review
-- comment: a text comment describing their opinion of the reviewable
--  emojis: the emoji that the user reviewed with
-- created: the timestamp that the user created the review
CREATE TABLE review (
	id serial,

    title text,
    comment text,
    emojis text NOT NULL,
    created timestamp default now(),

    app_user_id int NOT NULL,
    reviewable_id int NOT NULL,

	CONSTRAINT review_primary_key PRIMARY KEY (id),
    CONSTRAINT review_user_foreign_key FOREIGN KEY (app_user_id) REFERENCES app_user (id),
    CONSTRAINT review_reviewable_foreign_key FOREIGN KEY (reviewable_id) REFERENCES reviewable (id),

    UNIQUE (app_user_id, reviewable_id)
);