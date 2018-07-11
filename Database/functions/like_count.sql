CREATE OR REPLACE FUNCTION like_count ("id" int)
RETURNS int AS $$
    SELECT COUNT(*)::int FROM review_like where app_user_id = $1;
$$ LANGUAGE SQL;