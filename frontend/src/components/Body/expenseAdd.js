import { useEffect, useState } from "react";
import { SPENDO_APP_BASE_URL } from "../../config";

const ExpenseAdd = ({ editingExpense, onSave }) => {
  const [title, setTitle] = useState("");
  const [description, setDescription] = useState("");
  const [amount, setAmount] = useState("");

  // const AddExpense = async () => {
  //   // Validation on client side
  //   if (title == "" || description == "" || amount == "") {
  //     return;
  //   }

  //   const expense = {
  //     title: title,
  //     description: description,
  //     amount: amount,
  //   };

  //   const url = SPENDO_APP_BASE_URL + "/api/expense/add";
  //   try {
  //     const response = await fetch(url, {
  //       method: "POST",
  //       headers: {
  //         "Content-Type": "application/json",
  //       },
  //       body: JSON.stringify(expense),
  //     });
  //   } catch (error) {
  //     console.log(error.Message);
  //   }
  // };

  const handleSubmit = async () => {
    if (!editingExpense && (title == "" || description == "" || amount == "")) {
      return;
    }

    let expense = {};
    if (title != "") expense.title = title;
    if (description != "") expense.description = description;
    if (amount != "") expense.amount = amount;

    const url = editingExpense
      ? `${SPENDO_APP_BASE_URL}/api/expense/update/${editingExpense.id}`
      : `${SPENDO_APP_BASE_URL}/api/expense/add`;

    const method = editingExpense ? "PUT" : "POST";
    try {
      const response = await fetch(url, {
        method: method,
        headers: {
          "Content-Type": "application/json",
        },
        body: JSON.stringify(expense),
      });
    } catch (error) {
      console.log(error.Message);
    }
  };

  useEffect(() => {
    if (editingExpense) {
      setTitle(editingExpense.title);
      setDescription(editingExpense.description);
      setAmount(editingExpense.amount);
    } else {
      setTitle("");
      setDescription("");
      setAmount("");
    }
  });

  return (
    <div id="expInp">
      <input
        type="text"
        name="expTitle"
        placeholder="Title"
        value={title}
        onChange={(e) => setTitle(e.target.value)}
      />
      <input
        type="text"
        name="expdescription"
        placeholder="description"
        value={description}
        onChange={(e) => setDescription(e.target.value)}
      />
      <input
        type="number"
        name="expAmt"
        placeholder="Amount"
        value={amount}
        onChange={(e) => setAmount(e.target.value)}
      />
      <button type="submit" onClick={handleSubmit}>
        {editingExpense ? "Update" : "Submit"}
      </button>
    </div>
  );
};

export default ExpenseAdd;
