Program unafs;
{$APPTYPE CONSOLE}
Uses Windows;

Type

  TAFSHeader = Packed Record
     Sign: Array[0..3] Of Char;
    Count: Longword;
  End;

  TAFSRecord = Packed Record
    Offs: Longword;
    Size: Longword;
  End;

  TAFSDir = Packed Record
    Name: Array[0..31] Of Char;
    Y, M, D, H, I, S: Word; // Year, Month, Day, Hour, mInute, Seconds
    Size: Longword;
  End;

Var
   Fl, F: File;
   I, Sz: Longword;
       P: Pointer;
       S: String;
      HD: TAFSHeader;
      AR: TAFSRecord;
     Dir: ^TAFSDir;
     TOC: ^TAFSRecord;
  TM, TL: TFileTime;
      SM: TSystemTime;
Begin
  WriteLn('Unpacker for .AFS archives');
  WriteLn('(c) CTPAX-X Team 2011');
  WriteLn('http://www.CTPAX-X.ru/');
  WriteLn;

  If ParamCount <> 1 Then
  Begin
    WriteLn('Usage: unafs filename.afs');
    Exit;
  End;

  AssignFile(Fl, ParamStr(1));
  {$I-}
  FileMode:=0;
  Reset(Fl, 1);
  FileMode:=2;
  {$I+}
  If IOResult <> 0 Then
  Begin
    WriteLn('Can''t open input file: ' + ParamStr(1));
    Exit;
  End;

  BlockRead(Fl, HD, SizeOf(HD));

  If HD.Sign <> 'AFS' Then
  Begin
    WriteLn('This is not a .AFS archive!');
    CloseFile(Fl);
    Exit;
  End;

  Sz:=HD.Count * SizeOf(TOC^);
  GetMem(TOC, Sz);
  BlockRead(Fl, TOC^, Sz);
  // check Dir offset right after TOC
  BlockRead(Fl, AR, SizeOf(AR));
  // if no - go to the end of TOC padding
  If AR.Offs = 0 Then
  Begin
    Seek(Fl, TOC^.Offs - 8);
    BlockRead(Fl, AR, SizeOf(AR));
  End;
  Seek(Fl, AR.Offs);
  GetMem(Dir, AR.Size);
  BlockRead(Fl, Dir^, AR.Size);

  // unused
  SM.wDayOfWeek:=0;
  SM.wMilliseconds:=0;
  For I:=1 to HD.Count Do
  Begin
    If AR.Offs <> 0 Then S:=Dir^.Name
    Else // Grandia2, create a custom name for unpacking file
    Begin
      Str(I, S);
      While Length(S) < 8 Do S:='0' + S;
      S:=S + '.UNP';
    End;
    Write(S);
    Seek(Fl, TOC^.Offs);
    GetMem(P, TOC^.Size);
    BlockRead(Fl, P^, TOC^.Size);
    AssignFile(F, S);
    ReWrite(F, 1);
    BlockWrite(F, P^, TOC^.Size);

    If AR.Offs <> 0 Then
    Begin
      SM.wYear:=Dir^.Y;
      SM.wMonth:=Dir^.M;
      SM.wDay:=Dir^.D;
      SM.wHour:=Dir^.H;
      SM.wMinute:=Dir^.I;
      SM.wSecond:=Dir^.S;

      SystemTimeToFileTime(SM, TM);
      LocalFileTimeToFileTime(TM, TL);
      SetFileTime(PHandle(@F)^, @TL, @TL, @TL);
    End;

    CloseFile(F);
    FreeMem(P, TOC^.Size);
    WriteLn;
    Inc(Dir);
    Inc(TOC);
  End;
  Dec(Dir, HD.Count);
  Dec(TOC, HD.Count);

  FreeMem(Dir, AR.Size);
  FreeMem(TOC, Sz);
  CloseFile(Fl);
End.
