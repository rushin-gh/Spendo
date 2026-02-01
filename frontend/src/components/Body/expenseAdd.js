import { useEffect, useState } from "react";
import { AddExpense, UpdateExpense } from "../../utils/expenseFunctions";

const ExpenseAdd = ({ editingExpense, setEditingExpense, loadExpenses }) => {
  const [formData, setFormData] = useState({
    title: "",
    description: "",
    amount: "",
  });

  // Populate form when editing an expense
  useEffect(() => {
    if (editingExpense) {
      setFormData({
        title: editingExpense.title,
        description: editingExpense.description,
        amount: editingExpense.amount,
      });
    } else {
      resetForm();
    }
  }, [editingExpense]);

  const resetForm = () => {
    setFormData({ title: "", description: "", amount: "" });
  };

  const handleInputChange = (field, value) => {
    setFormData((prev) => ({ ...prev, [field]: value }));
  };

  const validateForm = () => {
    return formData.title && formData.description && formData.amount;
  };

  const handleSubmit = async () => {
    if (!validateForm()) {
      alert("Please fill all fields");
      return;
    }

    try {
      if (editingExpense) {
        await UpdateExpense(editingExpense.id, formData);
        setEditingExpense(null);
      } else {
        await AddExpense(formData);
        resetForm();
      }
      await loadExpenses();
    } catch (error) {
      console.error("Error submitting expense:", error);
      alert("Failed to save expense. Please try again.");
    }
  };

  const handleCancel = () => {
    setEditingExpense(null);
    resetForm();
  };

  return (
    <div id="expInp">
      <input
        type="text"
        name="expTitle"
        placeholder="Title"
        value={formData.title}
        onChange={(e) => handleInputChange("title", e.target.value)}
      />
      <input
        type="text"
        name="expDescription"
        placeholder="Description"
        value={formData.description}
        onChange={(e) => handleInputChange("description", e.target.value)}
      />
      <input
        type="number"
        name="expAmount"
        placeholder="Amount"
        value={formData.amount}
        onChange={(e) => handleInputChange("amount", e.target.value)}
      />

      <button type="button" onClick={handleSubmit}>
        {editingExpense ? "Update" : "Add"}
      </button>

      {editingExpense && (
        <button type="button" onClick={handleCancel}>
          Cancel
        </button>
      )}
    </div>
  );
};

export default ExpenseAdd;
