import { useEffect, useState } from "react";
import { AddExpense, UpdateExpense } from "../../utils/expenseFunctions";
import { ExpenseBtns } from "../../utils/constants";

const ExpenseAdd = ({ editingExpense, setEditingExpense, loadExpenses }) => {
  const [title, setTitle] = useState("");
  const [description, setDescription] = useState("");
  const [amount, setAmount] = useState("");

  useEffect(() => {
    handleInputs();
  }, [editingExpense]);

  const handleButtonClicks = async (btn, expId) => {
    if (btn == ExpenseBtns.add) {
      await AddExpense({
        title: title,
        description: description,
        amount: amount,
      });
      setValues({ title: "", description: "", amount: "" });
      loadExpenses();
    } else if (btn == ExpenseBtns.update) {
      await UpdateExpense(expId, {
        title: title,
        description: description,
        amount: amount,
      });
      setEditingExpense(null);
      loadExpenses();
    } else if (btn == ExpenseBtns.cancel) {
      setEditingExpense(null);
    }
  };

  const handleInputs = () => {
    var values = {
      title: editingExpense ? editingExpense.title : "",
      description: editingExpense ? editingExpense.description : "",
      amount: editingExpense ? editingExpense.amount : "",
    };

    setValues(values);
  };

  const setValues = (exp) => {
    setTitle(exp.title);
    setDescription(exp.description);
    setAmount(exp.amount);
  };

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
      {!editingExpense && (
        <button
          type="submit"
          onClick={() => handleButtonClicks(ExpenseBtns.add)}
        >
          {ExpenseBtns.add}
        </button>
      )}
      {editingExpense && (
        <>
          <button
            type="submit"
            onClick={() =>
              handleButtonClicks(ExpenseBtns.update, editingExpense.id)
            }
          >
            {ExpenseBtns.update}
          </button>
          <button
            type="submit"
            onClick={() => handleButtonClicks(ExpenseBtns.cancel)}
          >
            {ExpenseBtns.cancel}
          </button>
        </>
      )}
    </div>
  );
};

export default ExpenseAdd;
