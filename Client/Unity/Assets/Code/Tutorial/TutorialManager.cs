﻿namespace KS.PizzaEmpire.Unity
{	
	using UnityEngine;
	
	public class TutorialManager
	{
		private static volatile TutorialManager instance;
		private static object syncRoot = new Object();
		
		private TutorialStage[] stages;
		private int currentStageIndex;
		private TutorialStage currentStage;
	
		private bool allDone = false;
	
		private TutorialManager() { }
		
		/// <summary>
		/// Provides the Singleton instance of the RedisCache
		/// </summary>
		public static TutorialManager Instance
		{
			get
			{
				if (instance == null)
				{
					lock (syncRoot)
					{
						if (instance == null)
						{
							instance = new TutorialManager();
						}
					}
				}
				return instance;
			}
		}
	
		public void Initialize()
		{
			stages = new TutorialStage[2];
			TutorialStage stage;
			TutorialGUI gui;
			int cs = 0;
			
			// tutorial stage 0
			// INTRO
			stage = new TutorialStage();
			gui = new TutorialGUI();
			gui.Text = "Hey kiddo, it’s your Uncle Louie, how’s it going? I see the old man left you his restaurant. You know he never did manage to make something of this place, but I have faith in you, kid.";
			gui.ShowNextButton = true;
			stage.GUI = gui;
			stages[cs++] = stage;
			
			// tutorial stage 1
			// INTRO
			stage = new TutorialStage();
			gui = new TutorialGUI();
			gui.Text = "Don’t worry, I’ll help show you the ropes, so you can build the biggest pizza empire in this city!";
			gui.ShowNextButton = true;
			stage.GUI = gui;
			stages[cs++] = stage;
			
			currentStageIndex = 0;
			currentStage = stages[0];
			
			/*
			messages = new List<string>
			{
				// Intro
				"Hey kiddo, it’s your Uncle Louie, how’s it going? I see the old man left you in charge of this place. You know he never did manage to make something of this place, but I have faith in you, kid.",
				"Don’t worry, I’ll help show you the ropes, so you can build the biggest pizza empire in this city!",
				//Ordering ingredients tutorial
				"The first thing we need to do is call the wholesaler and order some flour. You can’t make pizza dough without flour, and the dough is the secret to the best tasting pizza!",					
				"To phone the wholesaler tap on the phone icon. Tap and drag the white flour icon into your shopping cart. Tap the checkmark when you’re done.",
				//After player orders flour
				"You did good kid. You shouldn’t have to wait too long to get your flour, the wholesaler is just around the corner. Just let me know when it gets here I’ll help you unload it.",
				//Wiping tables tutorial
				"In the meantime it looks like we have some cleaning up to do around here. To clean a table, tap on it and then take the cloth and wipe it. ",
				//After tables have been wiped
				"Good work! Tables will get dusty over time on their own and customers will also mess them up. Customers won’t sit in your restaurant to be served unless the table is clean!",
				"I hope you didn’t think that that owning a pizza restaurant was going to be all fun and games? It’s like my father always said “owning a pizza restaurant is not all fun and games… “ hmm … I forgot the rest. ",
				//Ordering tomatoes tutorial
				"You need great dough to make great pizza, but the sauce is just as important! If you want to make the best sauce you’re going to want fresh tomatoes. Luckily for us, there are local farmers who will deliver right to your restaurant.",
				"To order tomatoes, tap on the phone icon. Tap on the vegetables tab. Now add the tomatoes to your shopping cart like you did with the flour. When you’re done tap the checkmark.",
				//After ordering tomatoes
				"Way to go kiddo, you’re learning how to run a business! It takes a little longer to get stuff from the farm, but those tomatoes will be before you know it.",
				//Cleaning Dishes Tutorial
				"It seems you inherited another mess this time its in the kitchen. The dishes need to be cleaned if you want to serve customers. Dishes will accumulate as customers eat your pizza.. You’ll need to keep the dishes clean in order to keep working!",
				"To wash dishes, tap the dishes that have accumulated in the sink and then take the soap and scrub it over the dishes",
				//After dishes have been wiped
				"Gee you’re a hard worker. Keep this determination and your pizza restaurants are going to be all over this city in no time.",
				//When flour arrives / Receiving ingredients tutorial
				"Well look at that! Your flour is here. I told you it wouldn’t take long. To unload the flour, tap the flour bag in the back of the delivery van and drag it into your restaurant.",
				//After dragging flour
				"Boy that’s heavy. Good thing your Uncle Louie is around to help! You’re going to need to be able to keep track of the ingredients you have on hand so I’ll show you how.",
				//Viewing ingredients tutorial
				"Tap the inventory icon to view at the ingredients you have in the restaurant.",
				//After viewing ingredients
				"You’re a fast learner! There is only so much space in the restaurant so you’re going to have to keep a low inventory of ingredients and order them often.",
				//More dough ingredients tutorial
				"Oh swizzle sticks! I completely forgot! In order to make dough you’re going to need salt and yeast. Better get that wholesaler back on the phone!",		
				"Tap the phone icon and drag one salt and one yeast order to your cart and tap the checkmark icon when you’re done.",
				//After salt and yeast have been ordered
				"Sorry about the confusion, kid. Don’t worry, with my help, you will be making pizzas in no time!",
				"Remember you’re just a little guy in the pizza game so the wholesaler and farmer only have room in their  delivery vehicles for two items at a time. Once your restaurant becomes established you will be able to order more ingredients at once.",
				//Fridge tutorial
				"We can store things like flour but spoilable ingredients like tomatoes need to be stored in the fridge so they don’t go bad.",		
				"Tap on the phone icon and tap the equipment tab. Drag the fridge into your kitchen. When you’ve decided on a place for it, tap the checkmark icon.",
				//After installing the fridge
				"The fridge looks good there kid. This is starting to look like a pizza restaurant!",
				"You don’t have enough money to buy the biggest fridge available, but over time you can save up to buy a fridge with more space.",
				//When tomatoes arrive
				"Ah look at those juicy red tomatoes that the farmer just brought. Now that’s what I call high quality!",			
				"Tap the tomatoes in the back of the farmers truck and drag them into your fridge to store them.",
				@"			
			After dragging the tomatoes the fridge
			Now your tomatoes will be happy! It’s like my father always said “happy tomatoes make happy pizza which makes happy customers which makes…”. hmm I forgot the rest.
			
			You can look in your fridge any time that you want to see what’s available.
			
			Tap the storage icon to bring up the storage window. Tap the fridge tab to look at the ingredients you have stored in the fridge.
			More pizza sauce ingredients
			In order to make the best pizza sauce, we’re going to need more than just tomatoes. Get the farmer to bring you some fresh basil and onions.
			
			Tap the phone icon. Tap on the vegetables tab. Drag one onion and one basil order to your cart and tap the checkmark icon when you’re done.
			After ordering pizza sauce ingredients
			Mmmm I can already taste the pizza sauce. It’s going to be epic!
			Cheese tutorial
			Everything is going perfectly. You’re a natural at this business kid. 
			
			We have ordered almost all of the ingredients we need. But there’s one very important ingredient that we’re still missing.
			
			Cheeeeeeeeeeese! 
			
			It’s like my father always said “the dough gives the pizza it’s character, the sauce gives the pizza it’s pizzaz and the cheese gives…” hmmm … I forgot the rest.
			
			Luckily there is cheese farm in this area that can deliver you fresh cheese.
			
			Tap the phone icon. Tap on the dairy tab. Drag an order of mozzarella cheese to your cart and tap the checkmark icon when you’re done.
			When salt and yeast arrive
			Looks like the salt and yeast arrived. Drag it from the back of the delivery van like you did with the flour.
			After unloading the salt and yeast
			This is a momentous occasion! You have all of the ingredients to make our first batch of pizza dough. Are you excited? 
			Purchase dough maker tutorial
			In order to make pizza dough you’re going to need a dough mixer.  Tap on the phone icon and tap the equipment tab. Drag the dough mixer into your kitchen. When you’ve decided on a place for it, tap the checkmark icon.
			After installing the dough mixer
			I see you put the dough mixer in the perfect spot. Good thinking kid! 
			Dough making tutorial
			Now it’s time to make our first batch of dough. Tap on the dough mixer then drag the kind of dough you want to make into one of the mixers slots.
			After dragging dough icon
			We only have ingredients for white dough right now but you will learn other recipes soon enough! You will only be able to drag the dough that you currently have ingredients for.
			
			This is an entry level dough mixer, but it’s all that you could afford with your tight budget. Once the customers start rolling in you can buy a better mixer that can process more ingredients.
			
			Oh my! I almost forgot! We’re not done yet. We need to fire up the mixer. Twirl your finger on the dough mixer to start it mixing.
			After starting the mixer
			Ahhh there we go. There’s nothing quite like the sound of a dough mixer mixing up fresh dough. It sounds like… delicioso!
			
			Mixing dough takes time. You have to wait just the right amount. When the mixer is done it’s job you can come back and collect the dough.
			Re ordering supplies tutorial
			I’m going to remind of this in case you forget. When you use your ingredients to create more complicated you need to re-order from your suppliers. Make sure you order more flour, salt, and yeast or you won’t be able to make more dough!
			Cooking range tutorial
			Wow it’s amazing how much progress you’ve made in such a short period of time! 
			
			Just thinking ahead here kid, you’re going to need a stove in order to prepare all of your delicious pizza sauce.
			
			Tap on the phone icon and tap the equipment tab. Drag the cooking range into your kitchen. When you’ve decided on a place for it, tap the checkmark icon.
			After installing the cooking range
			Ah yes that’s where the magic will happen. 
			
			La la la la we’re going to make-a-the sauce. 
			
			…
			
			Sorry I got carried away.
			When the basil and onions arrive
			Today just keeps getting better. The farmer just arrived with the rest of the ingredients for your pizza sauce! 
			
			Tap and drag the basil and onions to the fridge to store them.
			When the basil and onions have been stored 
			Yippeee! We have everything we need to make our pizza sauce. This is going to be your signature. People will come from miles around to get a taste of this.
			
			Tap on the cooking range then drag the kind of sauce you want to make into one of the range’s slots. 
			After dragging pizza sauce icon
			We only have the ingredients for a basic sauce right now but don’t worry you will create masterpieces as we go.
			
			Remember it’s very important to mix the sauce thoroughly.  It’s like my father always said “mixing sauce is like other things that you need to do a lot of….“ hmmmm….. I forgot the rest. 
			
			Twirl your finger on the cooking range to get the sauce really cooking!
			After starting the sauce cooking
			It smells … delicioso! Now we have to be patient. Good sauce takes good time.
			Re ordering supplies tutorial
			Ok kid, I lied. I’m going to remind you again in case you forget. Remember to order more ingredients so that you can make more sauce!
			When the cheese arrives
			The cheese is here from the cheese farm. Be a nice boy and help me unload it into the fridge.
			
			Tap the cheese and drag it from the cheese farm truck into the fridge.
			Purchase cheese grater tutorial
			Now that we have a nice big brick o’ cheese, I can show you how to get the cheese ready to actually use on the pizza. 
			
			You’re going to use an ancient technique called “grating” the cheese. 
			
			What’s that? You’ve heard of this technique before? How is that possible, I thought it was a family secret!!!
			
			Tap on the phone icon and tap the equipment tab. Drag the cheese grater into your kitchen. When it’s in a spot that looks good to you, tap the checkmark icon.
			After installing the cheese grater
			Perfecto! 
			
			Tap on the cheese grater and drag the cheese you want to grate into one of the cheese grater’s slots.
			After dragging cheese
			Ready to fire up the machine?
			
			Move your finger back and forth over the cheese grater to start the grating process. Watch your fingers!
			After starting cheese grater
			Fresh cheese. Yummy! This is going to make all of the difference for your world class pizzas!
			When the dough is ready
			Wow!! Your first batch of dough. What a proud moment. Let’s enjoy this moment together. 
			
			…
			
			…
			
			zZZZzzZZzZZZ
			
			Wha?! Oh sorry I fell asleep. Running a pizza restaurant is hard work!
			
			Tap the dough mixer then tap and drag the prepared dough into the fridge for storage.
			Once the dough is dragged to the fridge
			You know I don’t want to sound like a broken record but it’s important to remember to keep your chain of ingredients flowing. It’s like my father always said: “You need ingredients from your suppliers to make your dough sauce and toppings, and you need those to make your pizzas”. 
			
			Wow.. I remembered!!!
			Tutorial on mopping the floor
			Look over here, the floor is filthy. I know you’re not even open yet  but you’re not going to attract many customers if the restaurant is in this condition.
			
			To clean up dirty areas of the floor, tap on the dirt and then swipe the mop over the area to clean.
			After mopping the floor
			Presto! Nice and shiny. That wasn’t too hard was it? The floor is going to get dirty with all of the foot traffic you’re going to have, not to mention work in the kitchen!
			When the pizza sauce is ready
			Look at that magnificent pot of pizza sauce. You’ve outdone yourself kid. Very impressive for a first timer.
			
			Tap the cooking range then tap and drag the pizza sauce to the fridge for storage.
			When the grated cheese is ready
			Look at all of that wonderful cheese. I love cheese.. mmmm cheeese
			
			Tap the cheese grater, then tap and drag the grated cheese to the fridge for storage.
			When dough, pizza sauce and cheese are ready
			Can you believe it? You have all of the components you need to make your first pizza! But first we need to install a pizza oven.
			Purchase oven tutorial
			Tap on the phone icon and tap the equipment tab. Drag the oven into your kitchen. When you’ve decided on a place for it, tap the checkmark icon.
			After installing the oven
			Hmmm, what are we waiting for lets make a pizza!!!
			Pizza making tutorial
			This is the moment you’ve been waiting for. Your first pizza. Sigh I remember my first pizza. I was a lot skinnier back then…
			
			…
			
			But let’s get back to the task at hand shall we?
			
			Tap the pizza assembly area. Tap the dough tab to see the doughs you have available. Drag a dough to the assembly area.
			
			Then tap the sauces tab to see the sauces you have available. Drag a sauce to the assembly area.
			
			Finally tap the toppings tab to see the toppings you have available. Drag some toppings to the assembly area.
			
			When you’re sure you have the combination you want, tap the checkmark icon.
			When the checkmark icon has been tapped
			What a masterpiece! You’re going to be the most famous pizza chef in the city one of these days.
			
			When a pizza has been assembled you can either drag it from the pizza assembly area to the fridge for storage, or pop it right into the oven. Since we don’t have anything into the oven let’s start cooking it right away!
			
			Tap the oven and drag the pizza you want to cook into one of the oven’s slots. When you are ready tap the checkmark.
			When the checkmark icon has been tapped
			Your first pizza is in the oven. How does it feel, kid? 
			Start oven tutorial
			To start the oven, hold your finger over the oven to set the oven timer.
			Once oven has been started
			It’s time for me to teach you about a little something called “patience” It’s going to take a long time, but when this pizza is ready, it is going to be unbelievable.
			
			…
			
			Take it from me, waiting is the key. 
			
			…
			
			Yes waiting, that’s the name of the game. Yup!
			
			…
			
			I’ve been doing this for years so I have really learned how to be patient. You can learn a lot from me, you know.
			
			…
			
			I can’t take it any more!!! That pizza is driving me crazy. Let me tell you a little family secret that will make things go faster. You can spend coupons to speed up almost anything at your restaurant. 
			
			Tap the oven, then tap on the cooking pizza. Tap the coupon in lower corner and then tap the checkmark.
			
			Once the coupon has been used
			You see, it’s easy! And the pizza is ready instantly. Can you believe it?!
			
			Well kid, what can I say? The amount you have been able to accomplish in a short time is nothing short of remarkable.
			
			The restaurant is clean, you’ve made connections with different suppliers, you’ve installed a great amount of equipment. We’re almost ready to open for business and start getting customers!
			Computer Tutorial
			I got you a little something as a gift in order to help you with your grand opening. It’s a computer! You can use it to track the orders of your customers. I’ll show you how to use it when a customer arrives.
			Sign making
			There is just one more step before you’re ready to open. You need a sign! Something that will stand out and catch people’s attention.
			
			It’s like my father always said: “A good sign is like a good sign except that a sign….” .. hmm … I forgot the rest. 
			
			Tap the sign button in the lower corner to create your custom sign
			After sign button is tapped
			Tap any of the sign elements to edit that element. 
			
			You can choose the name, logo, background, and border of your sign. You can select different fonts, colors, materials, sizes and positions for the different parts of your sign.
			
			When you tap on an element the controls underneath the sign change to reflect the settings you can decide for that element.
			
			When you’re happy with the way your sign looks, tap the checkmark.
			After checkmark is tapped
			Congratulations! Look at how beautiful that sign looks at the front of your store. It brings a tear to my eye. *sniff sniff*. 
			
			If you want to change your sign at any time, tap the sign button in the corner.
			First customer arrives
			What’s that? Is that a… … … a.. .. .. a customer?!
			
			!!!
			
			Just stay calm… breathe… don’t worry your Uncle Louie is here to help you. I’ve been in this situation many times before. Don’t panic it will be ok… breathe… 
			
			… breathe… just breathe….
			
			Aiiiiiiiiiiiiiiiiiiiiiiiiiiiii  (running around in panic)
			Tap on the customer and they will show you what they want. Swipe one of the tables to show the customer where to sit.
			Customer sits down
			Phew! You’re going to have to get used to serving customers if you want to have a successful pizza restaurant. I know it’s going to be difficult but … you can do it!
			
			Tap the computer to show your current orders. 
			
			To complete an order tap on the order and then tap the checkmark.
			After checkmark has been tapped
			Bam! Another satisfied customer! Well I guess it’s your first satisfied customer but I have a feeling it’s the first of many.
			Pepperoni tutorial
			You make a great cheese pizza, but If you want to be successful you can’t rest on your laurels. 
			It’s like my father always said “when you add meat to a pizza something happens...“ hmm … I forgot the rest. 
			
			I know a great place where we can get fresh, local meat.
			
			To order pepperoni, tap on the phone icon. Tap on the meat tab. Now drag the pepperoni to your shopping cart. When you’re done tap the checkmark.
			After ordered pepperoni
			Nice one! Very soon you will have delicious pepperoni to add to your pizzas. I can’t wait!
			
			In the meantime we need to install a meat slicer so we can put the meat on the pizza.
			
			Tap on the phone icon and tap the equipment tab. Drag the meat slicer into your kitchen. When it’s in the right spot, tap the checkmark icon.
			After installing the meat slicer
			The meat slicer looks very nice there. It makes the kitchen look more… more meaty.
			Cola syrup tutorial
			Serving pizzas is one thing, but if you want to have a successful restaurant you have to provide something for your customers to drink.
			
			To order cola syrup, tap on the phone icon. Tap on the drinks tab. Now drag the cola syrup to your shopping cart. When you’re done tap the checkmark.
			
			After customer leaves
			Don’t forget to clean the tables and do the dishes after you serve customers. If you have a messy restaurant you won’t get as many customers.
			When pepperoni arrives
			Ohhhh your very first meat delivery, how exciting! 
			Tap the pepperoni and drag it from the meat truck into the fridge.
			After storing the pepperoni
			Let’s get that pepperoni ready to put on one of your pizzas.
			
			Tap on the meat slicer and drag the meat you want to slice into one of the meat slicer’s slots.
			After dragging pepperoni
			It’s time to slice and dice!
			
			Hiyyyaaaaaaa (wearing karate headband?!)
			
			Swipe your finger over the meat slicer to start the machine.
			After starting the meat slicer
			Look at you! I barely have to tell you anything any more and you already know what to do. Pretty soon you’re not going to need me any more.
			When pepperoni is done slicing
			Hey look the pepperoni is done! You should store it in the fridge so that it will stay fresh longer.
			
			Tap the meat slicer, then tap and drag the pepperoni to the fridge for storage.
			When the cola syrup arrives
			Oh boy this stuff cola syrup is heavy… I think I hurt my back… help!
			
			Tap the cola syrup and drag it from the back of the cola truck onto the fridge.
			Once cola syrup is stored
			If you want to turn that cola syrup into a yummy delicious cola drink you’re going to need a soda machine.
			
			Tap on the phone icon and tap the equipment tab. Drag the soda machine into your kitchen. Remember to tap the checkmark when it’s in a good spot.
			After installing the soda machine
			Wonderful! Now you can make delicious sodas for your customers. They are really going to appreciate that especially on those hot days.
			
			Tap on the soda machine and drag the cola syrup into one of the soda machine’s slots.
			
			After installing the soda machine
			Remember to turn the machine on!
			
			Twirl your finger on the soda machine to start making soda.
			When you have all of the ingredients to make a pepperoni pizza
			Hey kid, good news! You have all of the things you need to make a pepperoni pizza. Use the pizza assembly area to make a pepperoni pizza.
			When the pepperoni pizza is assembled
			Magnifico! I can tell this is going to be a hit. You know what to do with the pizza from here.
			
			I think I’ve almost helped you as much as I can. There’s just one more thing I want to show you. You can put vegetable toppings on your pizza but you need a vegetable slicer to prepare them.
			
			Tap on the phone icon and tap the equipment tab. Drag the vegetable slicer into your kitchen. Tap the checkmark when it’s right where you want it.
			When the vegetable slicer is installed
			Well kid, I’m getting tired. I think I’m getting too old to be doing this pizza business thing full time.We’ve made a good start here but the rest is up to you. Don’t worry I’ll be around to help out when I can, but it’s your show now. 
			
			It’s like my father always said “when it’s time to say goodbye, the best thing to do is ...“ hmm … I forgot the rest. 
			
			Good luck!
			"
			};			
			*/
		}
	
		/// <summary>
		/// Tries to advance to the next tutorial stage
		/// </summary>
		public void TryAdvance(GamePlayer player, GUIEvent guiEvent)
		{
			if (currentStage.GUI != null && currentStage.GUI.ShowNextButton)
			{
				return;
			}
			
			if ((currentStage.PlayerStateCheck == null || currentStage.PlayerStateCheck.IsTrue(player)) &&
				(currentStage.GUIEventCheck == null || currentStage.GUIEventCheck.IsSame(guiEvent)))
			{
				Advance();
			}
		}
		
		/// <summary>
		/// Advance to the next stage in the tutorial
		/// </summary>
		public void Advance()
		{
			currentStageIndex++;

			if (currentStageIndex >= stages.Length) {
				allDone = true;
			} else {
				currentStage = stages[currentStageIndex];
			}
		}
	
		/// <summary>
		/// Called when the tutorial manager should update
		/// and render
		/// </summary>
		public void OnGUI(GamePlayer player, GUIEvent guiEvent)
		{
			if (allDone) {
				return;
			}
	
			TryAdvance(player, guiEvent);
	
			if (currentStage.GUI == null) {
				return;
			}
				
			GUI.Box (new Rect (10, 10, 600, 90), currentStage.GUI.Text, GUIGameObject.CurrentStyle);
			
			if (currentStage.GUI.ShowNextButton)
			{
			 	if (GUI.Button(new Rect(490,70, 100,20), "Next"))
			 	{
					Advance();
			 	}
			}
		}			
	}
}