create or replace  trigger passport_insert_update
AFTER insert or update
ON public.passports 
for each row
execute procedure   passport_insert_update_tg();