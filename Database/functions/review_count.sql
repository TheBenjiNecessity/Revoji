CREATE OR REPLACE FUNCTION review_count ("id" int)
RETURNS int AS $$
    SELECT COUNT(*)::int FROM review where app_user_id = $1;
$$ LANGUAGE SQL;