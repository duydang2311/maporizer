﻿namespace Maporizer.ColorizerPrompts.ViewModels;

public class PromptViewModel
{
    public Command OKCommand { get; }
    public Command CancelCommand { get; }
    public string picked;
    public string entry;
    public string Picked
    {
        get => picked;
        set
        {
            if (picked != value)
            {
                picked = value;
                OKCommand.ChangeCanExecute();
            }
        }
    }
    public string Entry 
    {
        get => entry;
        set
        {
            if (entry != value)
            {
                entry = value;
                OKCommand.ChangeCanExecute();
            }
        }
    }
    public PromptViewModel()
    {
        picked = " ";
        entry = " ";
        OKCommand = new Command(OKCommandHandler, OKCanExecuteHandler);
        CancelCommand = new Command(CancelCommandHandler);
    }
    private void OKCommandHandler()
    {
        MessagingCenter.Send(this, "OK", new Models.PromptResultModel(Picked, byte.Parse(Entry)));
    }
    private void CancelCommandHandler()
    {
        MessagingCenter.Send(this, "Cancel");
    }
    private bool OKCanExecuteHandler()
    {
        return Picked.Length != 0 && byte.TryParse(Entry, out byte _);
    }
}
