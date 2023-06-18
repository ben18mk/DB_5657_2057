/*
	{0} - passenger_id
*/

CALL CountPassengerPaymentMethod({0}, @cash_count, @card_count, @total_payments);

SELECT
	@cash_count AS cash,
	@card_count AS card,
	@total_payments AS total