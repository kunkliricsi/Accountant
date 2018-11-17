package com.kunkliricsi.accountant.fragments;

import android.os.Bundle;
import android.support.annotation.Nullable;
import android.support.v4.app.DialogFragment;
import android.text.InputFilter;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.view.Window;
import android.widget.EditText;
import android.widget.ToggleButton;

import com.kunkliricsi.accountant.R;

import java.text.DecimalFormat;
import java.text.DecimalFormatSymbols;
import java.text.NumberFormat;
import java.util.Locale;

public class AddExpenseDialogFragment extends DialogFragment {

    private ToggleButton Cash;
    private ToggleButton Credit;
    private EditText Amount;

    private String formattedAmount;
    private Number amount;

    public static AddExpenseDialogFragment newInstance() {
        return new AddExpenseDialogFragment();
    }

    ToggleButton.OnClickListener toggleListener
            = new ToggleButton.OnClickListener() {
        @Override
        public void onClick(View v) {
            if (((ToggleButton) v) == Cash) {
                if (Cash.isChecked()) {
                    Credit.setChecked(false);
                }
                else {
                    Cash.setChecked(true);
                }
            }
            else {
                if (Credit.isChecked()) {
                    Cash.setChecked(false);
                }
                else {
                    Credit.setChecked(true);
                }
            }
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

        EditText amount = (EditText) view.findViewById(R.id.expense_amount);

        Cash = (ToggleButton) view.findViewById(R.id.expense_cash);
        Credit = (ToggleButton) view.findViewById(R.id.expense_credit);
        Amount = (EditText) view.findViewById(R.id.expense_amount);

        Cash.setOnClickListener(toggleListener);
        Credit.setOnClickListener(toggleListener);
        Amount.setOnFocusChangeListener(amountFocusChangeListener);

        return view;
    }
}
