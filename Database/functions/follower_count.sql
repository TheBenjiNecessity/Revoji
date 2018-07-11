CREATE OR REPLACE FUNCTION follower_count ("id" int)
RETURNS int AS $$
    SELECT COUNT(*)::int FROM follower where following_app_user_id = $1;
$$ LANGUAGE SQL;