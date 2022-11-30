Implement a Web-application for the fake (random) user data generation.

The single app page allows to:
1) select region (at least 3 different, e.g. Poland, USA, Georgia or anything you prefer)
2) specify the number of error per record (slider 0..10 + binded number field with max value limit at least 1000)
3) define seed value and [Random] button to generate a random seed

If the user change anything, the table below (20 records are generated again).

It's necessary to support infinite scrolling in the table (you show 20 records and if the user scroll down, you add next 10 record below).

The table show contain the following fields:
1) Index (1, 2, 3, ...)
2) Random identifier
3) Name + middle name + last name (in region format)
4) Address (in several possible formats, e.g. city+street+building+appartment or county+city+street+house)
5) Phone (again, it's great to have several formats, e.g. international or local ones)

Language of the names and address as well as phone codes or zip codes should be correleted to region. You need to generate random data that looks somehow realistically. So, in Poland — Polish, in USA - English or Spanish, etc.
What is error? It's data entry error emulation. The end user specify number of errors PER RECORD. If errors = 0, there are no errors in user data. If error = 0.5, every record contains an error with probability 0.5 (one error per two recods). 10 errors results in 10 errors in every record. Error number can be entered with a slider or field (they interconnected, if change one control, other is changed too).

You need to support 3 type of errors - delete character in random position, add random character (from a proper alphabet) in random position, swap near characters. Type of the error have to be chosen ramdomly with equal probabilities (when user specifies 1000 errors, "noisy user data" should not be too long or too short).