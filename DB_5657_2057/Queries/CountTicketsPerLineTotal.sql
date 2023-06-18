SELECT line_id, COUNT(*) AS count
FROM tlines
NATURAL JOIN tickets
GROUP BY line_id