SET NOCOUNT ON;

DECLARE @Categories TABLE
(
    CategoryId INT NOT NULL,
    CategoryName NVARCHAR(250) NOT NULL
);

-- Insert Categories
INSERT INTO dbo.Category ([Name], [Description], IsActive)
OUTPUT inserted.Id, inserted.[Name]
INTO @Categories (CategoryId, CategoryName)
VALUES
(N'Electronics',     N'Devices, gadgets, and accessories.', 1),
(N'Home & Kitchen',  N'Essentials for cooking and home living.', 1),
(N'Books',           N'Fiction, non-fiction, and reference materials.', 1),
(N'Fitness',         N'Workout gear and wellness items.', 1),
(N'Office Supplies', N'Products for productivity and organization.', 1),
(N'Toys & Games',    N'Entertainment for kids and families.', 1),
(N'Clothing',        N'Apparel for everyday and seasonal wear.', 1),
(N'Beauty',          N'Personal care and grooming products.', 1),
(N'Outdoor',         N'Equipment for outdoor adventures.', 1),
(N'Pet Supplies',    N'Food and accessories for pets.', 1);

-- Map Category IDs
DECLARE
    @Electronics INT,
    @HomeKitchen INT,
    @Books INT,
    @Fitness INT,
    @Office INT,
    @Toys INT,
    @Clothing INT,
    @Beauty INT,
    @Outdoor INT,
    @Pets INT;

SELECT @Electronics = CategoryId FROM @Categories WHERE CategoryName = N'Electronics';
SELECT @HomeKitchen = CategoryId FROM @Categories WHERE CategoryName = N'Home & Kitchen';
SELECT @Books       = CategoryId FROM @Categories WHERE CategoryName = N'Books';
SELECT @Fitness     = CategoryId FROM @Categories WHERE CategoryName = N'Fitness';
SELECT @Office      = CategoryId FROM @Categories WHERE CategoryName = N'Office Supplies';
SELECT @Toys        = CategoryId FROM @Categories WHERE CategoryName = N'Toys & Games';
SELECT @Clothing    = CategoryId FROM @Categories WHERE CategoryName = N'Clothing';
SELECT @Beauty      = CategoryId FROM @Categories WHERE CategoryName = N'Beauty';
SELECT @Outdoor     = CategoryId FROM @Categories WHERE CategoryName = N'Outdoor';
SELECT @Pets        = CategoryId FROM @Categories WHERE CategoryName = N'Pet Supplies';

-- Insert Products
INSERT INTO dbo.Product
(
    [Name],
    [Description],
    Price,
    StockQuantity,
    IsActive,
    CategoryId
)
VALUES
-- Electronics
(N'Wireless Earbuds',        N'Noise-isolating Bluetooth earbuds.',        79.99, 120, 1, @Electronics),
(N'USB-C Fast Charger',      N'65W fast charger for phones and laptops.',  49.99,  80, 1, @Electronics),
(N'4K Action Camera',        N'Waterproof camera with stabilization.',    199.99,  25, 1, @Electronics),
(N'Power Bank 20000mAh',     N'High-capacity portable charger.',            59.99,  60, 1, @Electronics),
(N'Mechanical Keyboard',     N'Tactile mechanical keyboard.',              129.99,  40, 1, @Electronics),

-- Home & Kitchen
(N'Nonstick Fry Pan',        N'12-inch PFOA-free frying pan.',              34.99,  55, 1, @HomeKitchen),
(N'Mixing Bowl Set',         N'Stainless steel nesting bowls.',             29.99,  35, 1, @HomeKitchen),
(N'Coffee Grinder',          N'Burr grinder with adjustable settings.',     89.99,  18, 1, @HomeKitchen),
(N'Chef Knife',              N'8-inch high-carbon steel knife.',            79.99,  22, 1, @HomeKitchen),
(N'Food Storage Containers', N'Leak-resistant container set.',              24.99,  70, 1, @HomeKitchen),

-- Books
(N'Clean Code',              N'Software craftsmanship best practices.',     39.99,  15, 1, @Books),
(N'Learning SQL',            N'Beginner guide to SQL databases.',            29.99,  30, 1, @Books),
(N'Design Patterns',         N'Object-oriented design principles.',         44.99,  12, 1, @Books),
(N'C# in Depth',             N'Modern C# programming techniques.',          49.99,  25, 1, @Books),
(N'Web API Design',          N'Building scalable REST APIs.',               34.99,  20, 1, @Books),

-- Fitness
(N'Yoga Mat',                N'Non-slip exercise mat.',                     24.99,  90, 1, @Fitness),
(N'Resistance Bands',        N'5-level resistance band set.',               19.99,  75, 1, @Fitness),
(N'Adjustable Dumbbells',    N'Space-saving dumbbell set.',                 249.99,  10, 1, @Fitness),
(N'Foam Roller',             N'Muscle recovery foam roller.',               29.99,  45, 1, @Fitness),
(N'Jump Rope',               N'Adjustable-speed jump rope.',                14.99,  65, 1, @Fitness),

-- Office Supplies
(N'Notebook Pack',           N'Set of 3 ruled notebooks.',                   12.99, 110, 1, @Office),
(N'Gel Pens',                N'12-pack smooth writing pens.',                9.99,  95, 1, @Office),
(N'Desk Organizer',          N'Multi-compartment organizer.',                29.99,  40, 1, @Office),
(N'Wireless Mouse',          N'Ergonomic wireless mouse.',                   24.99,  50, 1, @Office),
(N'Label Maker',             N'Portable label maker.',                       49.99,  14, 1, @Office),

-- Toys & Games
(N'Building Blocks',         N'Creative construction blocks.',               39.99,  60, 1, @Toys),
(N'Family Board Game',       N'Strategy game for all ages.',                 49.99,  28, 1, @Toys),
(N'1000-piece Puzzle',       N'High-quality puzzle.',                        19.99,  35, 1, @Toys),
(N'RC Car',                  N'Remote control rechargeable car.',            59.99,  20, 1, @Toys),
(N'Card Game',               N'Fast-paced family card game.',                14.99,  75, 1, @Toys),

-- Clothing
(N'Cotton T-Shirt',          N'Breathable cotton t-shirt.',                  19.99, 150, 1, @Clothing),
(N'Hoodie',                  N'Fleece-lined pullover hoodie.',               49.99,  70, 1, @Clothing),
(N'Slim Fit Jeans',          N'Stretch denim jeans.',                        59.99,  55, 1, @Clothing),
(N'Running Socks',           N'3-pack moisture-wicking socks.',              14.99,  90, 1, @Clothing),
(N'Baseball Cap',            N'Adjustable everyday cap.',                    19.99,  65, 1, @Clothing),

-- Beauty
(N'Facial Cleanser',         N'Gentle daily facial cleanser.',               15.99,  85, 1, @Beauty),
(N'Moisturizer SPF',         N'Lightweight moisturizer with SPF.',           21.99,  60, 1, @Beauty),
(N'Shampoo',                 N'Sulfate-free shampoo.',                       13.99,  70, 1, @Beauty),
(N'Conditioner',             N'Hydrating daily conditioner.',                13.99,  65, 1, @Beauty),
(N'Hand Cream',              N'Non-greasy moisturizing cream.',               9.99,  95, 1, @Beauty),

-- Outdoor
(N'Camping Lantern',         N'LED lantern with dimming modes.',              34.99,  40, 1, @Outdoor),
(N'Insulated Water Bottle',  N'24oz vacuum insulated bottle.',               29.99,  75, 1, @Outdoor),
(N'Hiking Backpack',         N'30L lightweight backpack.',                   89.99,  18, 1, @Outdoor),
(N'Portable Camp Stove',     N'Compact outdoor stove.',                      59.99,  12, 1, @Outdoor),
(N'Thermal Blanket',         N'Emergency thermal blanket.',                   9.99, 120, 1, @Outdoor),

-- Pet Supplies
(N'Dog Treats',              N'Grain-free training treats.',                 12.99,  80, 1, @Pets),
(N'Cat Wand Toy',            N'Interactive feather toy.',                     9.99,  60, 1, @Pets),
(N'Pet Bed',                 N'Washable cushioned pet bed.',                 69.99,  20, 1, @Pets),
(N'Leash & Collar Set',      N'Reflective durable nylon set.',               24.99,  35, 1, @Pets),
(N'Stainless Food Bowl',     N'Non-slip stainless bowl.',                    14.99,  70, 1, @Pets);
