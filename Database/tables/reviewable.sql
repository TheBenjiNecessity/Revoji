-- ===== reviewable: a thing to attach a review to =====
--       title: the main text displayed as a title
--        type: what kind of reviewable this is
            -- time/location dependant (event, concert)
            -- consumable (movie, product, food)
            -- service ongoing (cell phone plan, tv package)
            -- business (restaurant, cell carrier)
-- description: a short bio of the reviewable
--     content: a container for things like imagery (localized)
--        info: a container for things like:
            -- UPC Code for products
            -- Event start/end time
CREATE TABLE reviewable (
    id serial,
    title text NOT NULL,
    type text NOT NULL,
    title_image_url text,
    description text,
    content json,
    info json,

    CONSTRAINT reviewable_primary_key PRIMARY KEY (id)
);