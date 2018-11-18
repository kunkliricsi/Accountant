package com.kunkliricsi.accountant.model;

import java.util.Date;
import java.util.List;

public class Report {
    public int ID;
    public List<Expense> Expenses;
    public Date Start;
    public Date End;
    public boolean Evaluated;
    public Date DateOfEvaluation;
}
