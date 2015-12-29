WITH b AS (
	SELECT
		payload,
		payload->>'name' as name, 
		(payload->>'likes')::integer as likes,
		(payload->>'checkins')::integer as checkins,
		(payload->'location'->>'city') as location_city,
		(payload->'location'->>'country') as location_country,
		(payload->'location'->>'state') as location_state,
		(payload->'location'->>'street') as location_street,
		(payload->'location'->>'zip') as location_zip,
		(payload->'location'->>'latitude')::decimal as location_latitude,
		(payload->'location'->>'longitude')::decimal as location_longitude,
		(payload->>'link') as link,
		(payload->>'were_here_count')::integer as were_here,
		(payload->>'talking_about_count')::integer as talking_about,
		(payload->>'name') as name,
		(payload->>'id')::bigint as id
	FROM boxes
	WHERE payload->>'name' <> ''
)
SELECT b.*
FROM b
ORDER BY likes DESC;