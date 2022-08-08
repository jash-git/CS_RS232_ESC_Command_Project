[.NETCore] / [.Net6] 使用 Big5 中文編碼 + [String 轉 Big5 Byte Array/C# 熱敏印表機 中文列印]

資料來源: https://harry-lin.blogspot.com/2019/05/net-core-big5.html

解決 [.NETCore] / [.Net6] 使用 Big5 中文編碼 問題
	01.當使用 .Net Core/.Net6 執行 Encoding.GetEncoding("big5")時會發生Exception
	System.ArgumentException: ''big5' is not a supported encoding name. For information on defining a custom encoding, see the documentation for the Encoding.RegisterProvider method.'

	02.透過 nuget 進行安裝 System.Text.Encoding.CodePages

	03.在程式進入點，引用該功能
	Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

String 轉 Big5 Byte Array [C# 熱敏印表機 中文列印]
