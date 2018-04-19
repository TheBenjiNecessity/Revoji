-- Review
CREATE TABLE review (
	id serial,

    title text,
    comment text,
    emojis text,

    app_user_id int NOT NULL,
    reviewable_event_id int,
    reviewable_product_id int,
    reviewable_service_id int,
    reviewable_business_id int,

	CONSTRAINT review_primary_key PRIMARY KEY (id),
    CONSTRAINT review_user_foreign_key FOREIGN KEY (app_user_id) REFERENCES app_user (id),
    CONSTRAINT review_reviewable_event_foreign_key FOREIGN KEY (reviewable_event_id) REFERENCES reviewable_event (id),
    CONSTRAINT review_reviewable_product_foreign_key FOREIGN KEY (reviewable_product_id) REFERENCES reviewable_product (id),
    CONSTRAINT review_reviewable_service_foreign_key FOREIGN KEY (reviewable_service_id) REFERENCES reviewable_service (id),
    CONSTRAINT review_reviewable_business_foreign_key FOREIGN KEY (reviewable_business_id) REFERENCES reviewable_business (id)
);