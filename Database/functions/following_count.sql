CREATE OR REPLACE FUNCTION following_count ("id" int)
RETURNS int AS $$
    SELECT COUNT(*)::int FROM follower where follower_app_user_id = $1;
$$ LANGUAGE SQL;