package com.kunkliricsi.accountant.fragments;

import android.content.Context;
import android.os.Bundle;
import android.os.StrictMode;
import android.provider.ContactsContract;
import android.support.annotation.Nullable;
import android.support.design.widget.BaseTransientBottomBar;
import android.support.design.widget.Snackbar;
import android.support.v4.app.DialogFragment;
import android.text.InputFilter;
import android.view.KeyEvent;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.view.Window;
import android.view.inputmethod.EditorInfo;
import android.view.inputmethod.InputMethodManager;
import android.widget.AdapterView;
import android.widget.ArrayAdapter;
import android.widget.Button;
import android.widget.EditText;
import android.widget.Spinner;
import android.widget.SpinnerAdapter;
import android.widget.TextView;
import android.widget.ToggleButton;

import com.kunkliricsi.accountant.MainActivity;
import com.kunkliricsi.accountant.R;
import com.kunkliricsi.accountant.database.DatabaseApi;
import com.kunkliricsi.accountant.database.local.AccountantDatabase;
import com.kunkliricsi.accountant.database.local.entities.Category;
import com.kunkliricsi.accountant.database.local.entities.Expense;
import com.kunkliricsi.accountant.database.local.entities.ShoppingListItem;
import com.kunkliricsi.accountant.database.local.entities.enums.PayOption;
import com.kunkliricsi.accountant.database.network.NetworkManager;

import java.security.Key;
import java.text.DecimalFormat;
import java.text.DecimalFormatSymbols;
import java.util.ArrayList;
import java.util.Date;
import java.util.List;
import java.util.Locale;

public class AddExpenseDialogFragment extends DialogFragment {

    private AccountantDatabase database;

    private ToggleButton Cash;
    private ToggleButton Credit;
    private EditText Amount;
    private EditText editThief;
    private Button Shopping;
    private TextView Add;
    private TextView Cancel;
    private Spinner Spinner;

    private String formattedAmount;
    private Number amount = 0;

    private int currentlySelectedCategory;
    private ArrayList<ShoppingListItem> selectedShoppingList = new ArrayList<>();

    public static AddExpenseDialogFragment newInstance() {
        return new AddExpenseDialogFragment();
    }

    ToggleButton.OnClickListener toggleListener
            = new ToggleButton.OnClickListener() {
        @Override
        public void onClick(View v) {
            clearFocus();
            Cash.setChecked(false);
            Credit.setChecked(false);
            ((ToggleButton)v).setChecked(true);
        }

    };

    EditText.OnFocusChangeListener amountFocusChangeListener
            = new EditText.OnFocusChangeListener() {
        @Override
        public void onFocusChange(View v, boolean hasFocus) {
            String amountString = Amount.getText().toString();

            if (!amountString.equals("")) {
                if (amountString.contains("Ft")) {
                    amount = Integer.parseInt(amountString
                            .replaceAll(" ", "")
                            .replace("Ft", ""));
                    ((EditText)v).setFilters(new InputFilter[] { new InputFilter.LengthFilter(6)});
                    Amount.setText(amount.toString());
                }
                else {
                    amount = Integer.parseInt(amountString);

                    formattedAmount = formatCurrency(amount, "#,###,###").concat(" Ft");
                    ((EditText)v).setFilters(new InputFilter[] { new InputFilter.LengthFilter(10)});
                    Amount.setText(formattedAmount);
                }

            }

        }
    };

    TextView.OnClickListener onClickListener = new View.OnClickListener() {
        @Override
        public void onClick(View v) {
            clearFocus();
            if (amount.intValue() == 0)
                return;

            Expense e = new Expense();
            e.Amount = amount.intValue();
            if (Cash.isChecked()) e.PayOption = PayOption.Cash;
            else e.PayOption = PayOption.Credit;
            e.CategoryID = currentlySelectedCategory;
            e.DateOfPurchase = new Date();
            DatabaseApi.addExpense(e);
            e = database.expenseDao().getLastExpense();
            for (ShoppingListItem item : selectedShoppingList){
                item.ExpenseID = e.id;
                DatabaseApi.updateShoppingListItem(item);
            }
            dismiss();
        }
    };

    private String formatCurrency(Number value, String formatString) {
        DecimalFormatSymbols formatSymbols = new DecimalFormatSymbols(Locale.ENGLISH);
        formatSymbols.setGroupingSeparator(' ');
        DecimalFormat formatter = new DecimalFormat(formatString, formatSymbols);
        return formatter.format(value);
    }

    @Nullable
    @Override
    public View onCreateView(LayoutInflater inflater, @Nullable ViewGroup container,
                             @Nullable Bundle savedInstanceState) {
        View view = inflater.inflate(R.layout.fragment_add_expense, container, false);
        getDialog().getWindow().requestFeature(Window.FEATURE_NO_TITLE);

        database = AccountantDatabase.getInstance(getContext());

        Amount = view.findViewById(R.id.expense_amount);
        Cash = view.findViewById(R.id.expense_cash);
        Credit = view.findViewById(R.id.expense_credit);
        Amount = view.findViewById(R.id.expense_amount);
        editThief = view.findViewById(R.id.expense_focus_thief);
        editThief.setShowSoftInputOnFocus(false);
        Shopping = view.findViewById(R.id.expense_shopping_list);

        final String[] shoppingList = DatabaseApi.getAllUnpaidShoppingListItemNames();
        Shopping.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                clearFocus();
                ShoppingListDialogFragment d = new ShoppingListDialogFragment();
                d.names = shoppingList;
                d.selectedItems = selectedShoppingList;
                d.show(getFragmentManager(), "shoppinglist");
            }
        });

        Spinner = view.findViewById(R.id.spinner_category);
        String[] categories = DatabaseApi.getCategoryNames();
        ArrayAdapter<String> spinnerAdapter = new ArrayAdapter<>(getContext(), android.R.layout.simple_spinner_item,
                categories);

        spinnerAdapter.setDropDownViewResource(android.R.layout.simple_spinner_dropdown_item);
        Spinner.setAdapter(spinnerAdapter);
        Spinner.setOnItemSelectedListener(new AdapterView.OnItemSelectedListener() {
            @Override
            public void onItemSelected(AdapterView<?> parent, View view, int position, long id) {
                clearFocus();
                currentlySelectedCategory = position;
            }

            @Override
            public void onNothingSelected(AdapterView<?> parent) {
                clearFocus();

            }
        });

        Add = view.findViewById(R.id.expense_add);
        Add.setOnClickListener(onClickListener);
        Cancel = view.findViewById(R.id.expense_cancel);
        Cancel.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                dismiss();
            }
        });

        Cash.setOnClickListener(toggleListener);
        Credit.setOnClickListener(toggleListener);
        Amount.setOnFocusChangeListener(amountFocusChangeListener);
        Amount.setOnEditorActionListener(new TextView.OnEditorActionListener() {
            @Override
            public boolean onEditorAction(TextView v, int actionId, KeyEvent event) {
                if (actionId == EditorInfo.IME_ACTION_DONE){
                    clearFocus();
                }
                return false;
            }
        });

        return view;
    }

    private void clearFocus(){
        Amount.clearFocus();
        editThief.requestFocus();
    }
}
