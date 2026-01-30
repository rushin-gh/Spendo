import { useEffect, useState } from "react";
import { AddExpense, UpdateExpense } from "../../utils/expenseFunctions";

const ExpenseAdd = ({ editingExpense, setEditingExpense, onSave }) => {
  const [title, setTitle] = useState("");
  const [description, setDescription] = useState("");
  const [amount, setAmount] = useState("");

  useEffect(() => {
    handleInputs();
  }, [editingExpense]);

  const handleSubmit = async (exp, expId) => {
    if (editingExpense) {
      UpdateExpense(expId, exp);
    } else {
      AddExpense(exp);
    }
  };

  const handleInputs = () => {
    var values = {
      title: editingExpense ? editingExpense.title : "",
      description: editingExpense ? editingExpense.description : "",
      amount: editingExpense ? editingExpense.amount : "",
    };

    if (editingExpense) {
      setTitle(values.title);
      setDescription(values.description);
      setAmount(values.amount);
    }
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
      <button type="submit" onClick={handleSubmit}>
        {editingExpense ? "Update" : "Submit"}
      </button>
      {editingExpense && (
        <button type="submit" onClick={() => setEditingExpense(null)}>
          Cancel
        </button>
      )}
    </div>
  );
};

export default ExpenseAdd;
