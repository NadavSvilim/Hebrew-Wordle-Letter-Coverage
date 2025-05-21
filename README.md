## Hebrew Letter Coverage Analysis

This project was inspired by the game *Wordle*, where players guess 5-letter words in a Bulls and Cows style and a youtube video of the mathematician Matt Parker.

I wondered: is it possible to find **four 5-letter Hebrew words** that together contain **20 out of the 22 Hebrew letters**?

### Project Focus

The main focus of this project was **optimizing runtime** using **bitwise operations** and **graph theory**.

---

### Project Components

- A dataset of Hebrew words used in Wikipedia, along with a count of how frequently each word appears.
- A script that:
  - **Filters** the dataset, writing only 5-letter words where each letter appears **at most once per word**.
  - **Parses** the remaining words and creates a representation for each word using a **22-bit binary number** — each bit represents one Hebrew letter. For example, the word "`באמצע`" would have bits **1st, 2nd, 13th, 16th, and 18th** set to 1, and the rest set to 0.
  - **Builds an non-directed graph** where:
    - Each word is a **vertex**
    - Two vertices are connected **if and only if** the bitwise **AND** of their representations equals 0 (i.e., the words **share no letters**).
  - **Searches for cliques** of size 4 or 3 — sets of words with no shared letters.

---

### Results

Unfortunately, the dataset did **not** contain four 5-letter Hebrew words that together cover 20 out of 22 letters.
However, the project was a fun and insightful challenge, applying algorithmic thinking, graph theory, and optimization techniques to solve a real-world puzzle.

---

### Technologies Used

- C#
- Bitwise operations
- Graph theory
- Custom word parsing logic

---

###Author

Nadav Svilim — Software Engineering Student @ BGU  
GitHub: [@NadavSvilim](https://github.com/NadavSvilim)
