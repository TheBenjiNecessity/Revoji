-- ===== reviewable: a thing to attach a review to =====
--       tp_id: the id given by third party api (barcode, imdbid, ...)
--       tp_name: the name of the third party for uniqueness('imdb', 'barcode')
--       title: the main text displayed as a title
--        type: what kind of reviewable this is
            -- time/location dependant (event, concert)
            -- consumable (movie, product, food)
            -- service ongoing (cell phone plan, tv package)
            -- business (restaurant, cell carrier)
-- description: a short bio of the reviewable
--     content: a container for things like imagery (localized)
--        info: a container for things like:
            -- Event start/end time
CREATE TABLE reviewable (
    id serial,
    tp_id text NOT NULL,
    tp_name text NOT NULL,
    title text NOT NULL,
    type text NOT NULL,
    title_image_url text,
    description text,
    content json,
    info json,

    CONSTRAINT reviewable_primary_key PRIMARY KEY (id),

    UNIQUE (tp_id, tp_name)
);