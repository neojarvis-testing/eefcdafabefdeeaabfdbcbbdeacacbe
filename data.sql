-- Dummy data for school_class table
INSERT INTO school_class (class_name, teacher_name, room_number, class_capacity)
VALUES 
('Mathematics', 'Mr. Smith', 'Room 101', 30),
('Science', 'Ms. Johnson', 'Room 102', 25),
('History', 'Mr. Thompson', 'Room 103', 20);

-- Dummy data for school_student table
INSERT INTO school_student (student_name, date_of_birth, class_id)
VALUES 
('John Doe', '2008-05-10', 1),
('Jane Smith', '2009-07-15', 2),
('Michael Johnson', '2008-12-20', 1),
('Emily Thompson', '2009-03-05', 3);

-- Dummy data for school_subject table
INSERT INTO school_subject (subject_name, teacher_name)
VALUES 
('Algebra', 'Mr. Brown'),
('Biology', 'Ms. Davis'),
('World History', 'Mr. Wilson');

-- Dummy data for school_result table
INSERT INTO school_result (student_id, subject_id, score)
VALUES 
(1, 1, 85.5),
(2, 2, 90),
(3, 1, 88),
(4, 3, 92.5);
