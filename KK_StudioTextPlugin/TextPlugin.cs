﻿using BepInEx.Logging;
using Studio;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using Logger = BepInEx.Logger;

namespace KK_StudioTextPlugin {
    static class TextPlugin {
        private static Material Font3DMaterial;
        internal static bool DisablePreview = true;
        private static readonly Dictionary<string, Font> DynamicFonts = new Dictionary<string, Font>();
        private static string[] FontList;
        //private static readonly string chineseTxt = "一丁七三下丈上丑丐不丙世丕且丘丞丟並丫中串丸凡丹主乃久么之尹乍乏乎乒乓乖乘乙九也乞乩乳乾亂了予事二于云井互五亙些亞亟亡交亦亥亨享京亭亮人仁什仃仆仇仍今介仄以付仔仕他仗代令仙仞仿伉伙伊伕伍伐休伏仲件任仰仳份企位住佇佗佞伴佛何估佐佑伽伺伸佃佔似但佣作你伯低伶余佝佯依侍佳使佬供例來侃佰併侈佩佻侖佾侏信侵侯便俠俑俏保促侶俘俟俊俗侮俐俄係俚俎俞倌倍倣俯倦倥俸倩倖倆值借倚倒們俺倀倔倨俱倡個候倘俳修倭倪俾倫倉偺偽停假偃偌做偉健偶偎偕偵側偷偏倏傢傍傅備傑傀傖傘傭債傲傳僅傾催傷傻傯僧僮僥僖僭僚僕像僑僱億儀僻僵價儂儈儉儒儘儔儐優償儡儲儷儼兀元允充兄光兇兆先兌克兕免兔兒兗兜兢入內全兩八六兮公共兵具其典兼冀冉冊再冒冑冕最冗冠冤冥冢冬冰冶冷冽凍凌准凋凜凝几凰凱凳凶凹出凸函刀刁刃分切刈刊列刑划刎別判利刪刨刻券刷刺到刮制剁剎剃削前剌剋則剖剜剔剛剝剪副割剴創剩剿剷剽劃劇劈劉劍劑力加功劣劫助努劬劾勇勉勃勁勒務勘動勞勝勛募勦勤勢勵勸勻勾勿包匆匈匍匐匏匕化北匙匝匡匠匣匪匯匱匹匿區匾十千午升卅仟半卉卒協卓卑南博卜卞卡占卦卯卮印危即卵卷卸卹卻卿厄厚原厝厥厭厲去參又叉友及反取叔受叛叟曼叢口可古右召叮叩叨叼司叵叫另只史叱台句叭吉吏同吊吐吁吋各向名合吃后吆吒吝吭吞吾否呎吧呆呃吳呈呂君吩告吹吻吸吮吵吶吠吼呀吱含吟味呵咖呸咕咀呻呷咄咒咆呼咐呱呶和咚呢周咋命咎咬哀咨哎哉咸咦咳哇哂咽咪品哄哈咯咫咱咻哨唐唁唷哼哥哲唆哺唔哩哭員唉哮哪哦唧商啪啦啄啞啡啃啊唱啖問啕唯啤唸售啜唬啣唳啻喀喧啼喊喝喘喂喜喪喔喇喋喃喳單喟唾喲喚喻喬喱啾喉嗟嗨嗓嗦嗎嗜嗇嗑嗣嗤嗯嗚嗡嗅嗆嗥嗾嘀嘛嘗嗽嘔嘆嘉嘍嘎嗷嘖嘟嘈嘮嘻嘹嘲嘿嘩噓噎噗噴嘶嘯嘰噙噫噹噩噤噸嘴噪器噥噱噯噬噢嚎嚀嚐嚅嚇嚏嚕嚮嚥嚨嚷嚶嚴嚼囁囀囂囈囊囉囌囑四囚因回囪困囤固圃圈國圍園圓團圖土圳地在圭圬圯坊坑址坍均坎圾坐坏垃坷坪坩坡坦坤坼垂型垠垣垢城垮埂埔埋埃域堅堊堆埠埤基堂堵執培堯堪場堤堰報堡塞塑塘塗塚塔填塌塭塊塢塵塾境墓墊塹墅墀墟增墳墜墮壁墾壇壅壕壓壑壙壘壞壟壢壤壩士壬壯壹壺壽夏夔夕外夙多夜夠夥夢夤大天夫太夭央失夷夸夾奉奇奈奄奔奕契奏奎奐套奘奚奢奠奧奪奩奮女奴奶妄奸妃好她如妁妝妒妨妞妣妙妖妍妤妓妊妥妾妻委妹妮姑姆姐姍始姓姊妯妳姒姜姘姿姣姨娃姥姪姚姦威姻娑娘娜娟娛娓姬娠娣娩娥娌娶婁婉婦婪婀娼婢婚婆婊婷媚婿媒媛嫁嫉嫌媾媽媼媳嫂媲嫡嫦嫩嫗嫖嫘嫣嬉嫻嬋嫵嬌嬝嬴嬰嬪嬤嬸孀子孑孓孔孕字存孝孜孚孟孤季孩孫孰孳孱孵學孺孽孿它宇守宅安完宋宏宗定官宜宙宛宣宦室客宥宰害家宴宮宵容宸寇寅寄寂宿密寒富寓寐寞寧寡寥實寨寢寤察寮寬審寫寵寶寸寺封射尉專將尊尋對導小少尖尚尤尬就尷尸尺尼局屁尿尾屈居屆屎屏屍屋屑展屐屠屜屢層履屬屯山屹岐岑岔岌岷岡岸岩岫岱岳峙峭峽峻峪峨峰島崁崇崆崎崛崖崢崑崩崔崙嵌嵐嵩嶄嶇嶝嶼嶺嶽巍巔巒巖川州巢工巨巧左巫差己已巳巴巷巽巾市布帆希帘帚帖帕帛帑帝帥席師常帶帳帷幅帽幀幌幛幣幕幗幔幢幟幫干平并年幸幹幻幼幽幾序庇床庚店府底庖庠度庫庭座康庸庶庵庾廊廁廂廉廈廓廖廢廚廟廝廣廠龐廬廳廷延建廿弁弄弈弊式弒弓弔引弘弗弛弟弦弧弩弭弱張強弼彆彈彌彎彗彙彝彤形彥彬彩彫彭彰影彷役往征彿彼很待徊律徇後徒徑徐得徙從徘御復循徨徬微徹德徵徽心必忙忖忘忌志忍忱快忝忠忽念忿怏怔怯怵怖怪怕怡性怒思怠急怎怨恍恰恨恢恆恃恬恫恪恤恙恣恥恐恕恭恩息悄悟悚悍悔悌悅悖恿患悉悠您惋悴惦悽情悻悵惜悼惘惕惆惟悸惚惑惡悲悶惠愜愣惺愕惰惻惴慨惱愎惶愉愀愚意慈感想愛惹愁愈慎慌慄慍愾愴愧慇愿態慷慢慣慟慚慘慶慧慮慝慕憂慼慰慫慾憧憐憫憎憬憚憤憔憲憑憩憊懍憶憾懂懊懈應懇懦懣懲懷懶懵懸懺懼懾懿戀戈戊戎戌戍成戒我或戕戚戛戟戡戢截戮戰戲戴戳戶房戾所扁扇扈扉手才扎打扔扒扣扛托抄抗抖技扶抉扭把扼找批扳抒扯折扮投抓抑承拉拌拄抿拂抹拒招披拓拔拋拈抨抽押拐拙拇拍抵拚抱拘拖拗拆抬拎拜挖按拼拭持拮拽指拱拷拯括拾拴挑拳挈拿捎挾振捕捂捆捏捉挺捐挽挪挫挨掠控捲掖探接捷捧掘措捱掩掉掃掛捫推掄授掙採掬排掏掀捻捩捨掣掌描揀揩揉揆揍插揣提握揖揭揮捶援揪換摒揚搓搾搞搪搭搽搬搏搜搔損搶搖搗撇摘摔撤摸摟摺摑摧摩摯摹撞撲撈撐撰撥撓撕撩撒撮播撫撚撬擅擁擋撻撼據擄擇擂操撿擒擔擎擊擘擠擰擦擬擱擴擲擾攆擺擻攀攏攘攔攙攝攜攤攣攫攪攬支收改攻放政故效敝敖救教敗啟敏敘敞敦敢散敬敲敵敷數整斂斃文斑斐斗料斜斟斡斤斥斧斫斬斯新斷方於施旁旅族旋旌旎旗旖既日旦早旨旬旭旱旺昔易昌昆昂明昀昏春昭映昧是星昨時晉晏晃晒晌晝晚晤晨晦普晰晴晶景暑智暗暉暇暈暖暢暨暮暫暴曆曉暹曙曖曠曝曦曰曲曳更曷書曹勗曾替會月有服朋朔朕朗望期朝朦朧木朮本未末札朽朴朱朵束李杏材村杜杖杞杉杭枋枕東果杳杷枇枝林杯杰板枉松析杵枚柿染柱柔某柬架枯柵柩柯柄柑枴柚查枸柏柞柳校核案框桓根桂桔栩梳栗桌桑栽柴桐桀格桃株桅栓梁梯梢梓梵桿桶梱梧梗械梃棄梭梆梅梔條梨梟棺棕棠棘棗椅棟棵森棧棹棒棲棣棋棍植椒椎棉棚榔業楚楷楠楔極椰概楊楨楫楞楓楹榆榜榨榕槁榮槓構榛榷榻榫榴槐槍榭槌樣樟槨樁樞標槽模樓樊槳樂樅樽樸樺橙橫橘樹橄橢橡橋橇樵機檀檔檄檢檜櫛檳檬櫃檻檸櫂櫥櫝櫚櫓櫻欄權欖欠次欣欲款欺欽歇歉歌歐歙歟歡止正此步武歧歪歲歷歸歹死歿殃殆殊殉殘殖殤殮殯殲段殷殺殼毀殿毅毆毋母每毒毓比毗毛毫毯毽氏民氐氓氖氛氟氣氧氨氦氤氫氮氯氳水永汁汀氾求汝汗汙江池汐汕汞沙沁沈沉沅沛汪決沐汰沌汨沖沒汽沃汲汾泣注泳沱泌泥河沽沾沼波沫法泓沸泄油況沮泗泅泱沿治泡泛泊泉泰洋洲洪流津洌洱洞洗活洽派洶洛浪涕消涇浦浸海浙涓浬涉浮浚浴浩涎涼淳淙液淡淌淤添淺清淇淋涯淑涮淞淹涸混淵淅淒渚涵淚淫淘淪深淮淨淆淄港游湔渡渲湧湊渠渥渣減湛湘渤湖湮渭渦湯渴湍渺測湃渝渾滋溉渙溢溯滓溶滂源溝滇滅溥溘溼溺溫滑準溜滄滔溪漳演滾漓滴漩漾漠漬漏漂漢滿滯漆漱漸漲漣漕漫漯澈漪滬漁滲滌漿潼澄潑潦潔澆潭潛潸潮澎潺潰潤澗潘濂澱澡濃澤濁澧澳激澹濘濱濟濠濛濤濫濯澀濬濡瀉瀋濾瀆濺瀑瀏瀛瀟瀨瀚瀝瀕瀾瀰灌灑灘灣灤火灰灶灼災灸炕炎炒炊炙炫為炳炬炯炭炸炮烊烘烤烙烈烏烹焉焊烽焙焚焦焰無然煮煎煙煩煤煉照煜煬煦煌煥煞熔熙煽熊熄熟熬熱熨熾燉燐燒燈燕熹燎燙燜燃燄燧營燮燦燥燭燬燴燻爆爍爐爛爨爪爬爭爰爵父爸爹爺爻爽爾牆片版牌牒牖牘牙牛牟牝牢牡牠牧物牲牯牴特牽犁犄犀犒犖犛犢犧犬犯狄狂狀狎狙狗狐狩狠狡狼狹狽狸狷猜猛猖猓猙猶猥猴猩猷獅猿猾獄獐獎獗獨獰獲獷獵獸獺獻玀玄率王玉玖玩玨玟玫玷珊玻玲珍珀玳班琉珮珠琅琊球理現琍琺琪琳琢琥琵琶琴瑯瑚瑕瑟瑞瑁琿瑙瑛瑜瑤瑣瑪瑰瑩璋璃璜璣璩環璦璧璽瓊瓏瓜瓠瓢瓣瓦瓶瓷甄甌甕甘甚甜生產甥甦用甩甬甫甭田由甲申男甸甽畏界畔畝畜畚留略畦畢異畫番當畸疇疆疊疋疏疑疝疙疚疫疤疥疾病症疲疳疽疼疹痊痔痕疵痢痛痣痙痘痞瘀痰瘁痲痱痺痿痴瘧瘍瘋瘉瘓瘠瘩瘟瘤瘦瘡瘴瘸癆療癌癖癘癒癢癥癩癮癬癱癲癸登發白百皂的皆皇皈皎皖皓皚皮皰皴皺皿盂盈盆盃益盍盎盔盒盛盜盞盟盡監盤盧盥盪目盯盲直省盹相眉看盾盼眩真眠眨眷眾眼眶眸眺睏睛睫睦睞督睹睪睬睜睥睨瞄睽睿睡瞎瞇瞌瞑瞠瞞瞟瞥瞳瞪瞰瞬瞧瞭瞽瞿瞻矇矓矗矚矛矜矢矣知矩短矮矯石矽砂研砌砍砰砧砸砝破砷砥砭硫硃硝硬硯碎碰碗碘碌碉硼碑磁碟碧碳碩磋磅確磊碾磕碼磐磨磚磬磷磺磴磯礁礎礙礦礪礬礫示社祀祁祆祉祈祇祕祐祠祟祖神祝祗祚祥票祭祺祿禁禎福禍禦禧禪禮禱禹萬禽禾私秀禿秉科秒秋秤秣秧租秦秩移稍稈程稅稀稜稚稠稔稟種稱稿稼穀稽稷稻積穎穆穌穗穡穢穫穩穴究空穹穿突窄窈窒窕窘窗窖窟窠窪窩窯窮窺竄竅竇竊立站童竣竭端競竹竺竿竽笆笑笠笨笛第符笙笞等策筆筐筒答筍筋筏筷節筠管箕箋筵算箝箔箏箭箱範箴篆篇篁篙簑築篤篛篡篩簇簍篾篷簫簧簪簞簣簡簾簿簸簽簷籌籃籍籐籠籟籤籬籮籲米粉粒粗粟粥粱粳粵粹粽精糊糕糖糠糜糞糢糟糙糧糯糸系糾紂紅紀紉紇約紡紗紋紊素索純紐紕級紜納紙紛絆絃統紮紹紼絀細紳組累終絞結絨絕紫絮絲絡給絢經絹綑綁綏綻綰綜綽綾綠緊綴網綱綺綢綿綵綸維緒緇締練緯緻緘緬緝編緣線緞緩綞縊縑縈縛縣縮績繆縷縲繃縫總縱繅繁織繕繞繚繡繫繭繹繩繪辮繽繼纂纏續纓纖纜缶缸缺缽罄罈罐罕罔罟置罩罪署罰罵罷罹羅羈羊羌羋美羔羞羚善義羨群羯羲羶羹羸羽羿翅翁翌翎習翔翕翠翡翟翩翰翱翳翼翹翻耀老考者耆而耐耍耒耘耕耙耗耜耳耶耽耿聊聆聖聘聞聚聱聲聰聯聳職聶聾聽聿肆肄肅肇肉肋肌肖肓肝肘肛肚育肺肥肢肱股肫肩肴肪肯胖胥胚胃胄背胡胛胎胞胤胱脂胰脅胭胴脆胸胳脈能脊脯脖脣脫脩腕腔腋腑腎脹腆脾腐腱腰腸腥腮腳腫腹腺腦膀膏膈膊腿膛膜膝膠膚膳膩膨臆臃膺臂臀膿膽臉膾臍臏臘臚臟臣臥臧臨自臭臬至致臺臻臼臾舀舂舅與興舉舊舌舍舐舒舔舛舜舞舟舢航舫舨般舵舷舶船艇艘艙艦艮良艱色艾芒芋芍芳芝芙芭芽芟芹花芬芥芻苧范茅苣苛苦茄若茂茉苒苗英茁苜苔苑苞苓苟茫荒荔荊茸荐草茵茴荏茲茹茶茗荀茱莎莞莘荸莢莖莽莫莒莊莓莉莠荷荻荼菩萃菸萍菠菅萋菁華菱菴著萊菰萌菌菽菲菊萸萎萄菜蒂葷落萱葵葦葫葉葬葛萼萵葡董葩蓉蒿蓆蓄蒙蒞蒲蒜蓋蒸蓀蓓蒐蒼蔗蔽蔚蓮蔬蔭蔓蔑蔣蔡蔔蓬蔥蓿蕊蕙蕈蕨蕩蕃蕉蕭蕪薪薄蕾薜薑薔薯薛薇藏薩藍藐藉薰藩藝藪藕藤藥藻藹蘑藺蘆蘋蘇蘊蘗蘭蘚蘸蘿虎虐虔處彪虛虜虞號虧虫虱虹蚊蚪蚓蚤蚩蚌蚣蛇蛀蚶蛄蚵蛆蛋蚱蚯蛟蛙蛭蛔蛛蛤蛹蜓蜈蜇蜀蛾蛻蜂蜃蜿蜜蜻蜢蜥蜴蜘蝕螂蝴蝶蝠蝦蝸蝨蝙蝗蝌螃螟螞螢融蟀蟑螳蟒蟆螫螻螺蟈蟋蟯蟬蟲蟻蠅蠍蟹蠔蠕蠣蠢蠡蠟蠱蠶蠹蠻血行衍術街衙衛衝衡衢衣初表衫衰衷袁袂袞袈被袒袖袍袋裁裂袱裟裔裙補裘裝裡裊裕裳褂裴裹裸製裨褚褐複褒褓褪褲褥褫褻褶襄褸襠襟襖襤襪襲襯西要覃覆見覓規視親覦覬覲覺覽觀角解觴觸言計訂訃記訐討訌訕訊託訓訖訪訝訣訥許設訟訛註詠評詞証詁詔詛詐詆訴診詫該詳試詩詰誇詼詣誠話誅詭詢詮詬詹誦誌語誣認誡誓誤說誥誨誘誑誼諒談諄誕請諸課諉諂調誰論諍諦諺諫諱謀諜諧諮諾謁謂諷諭謎謗謙講謊謠謝謄謨謹謬譁譜識證譚譎譏議譬警譯譴護譽讀變讓讒讖讚谷豁谿豆豈豉豌豎豐豔豕豚象豢豪豬豫豺豹貂貊貉貍貌貓貝貞負財貢販責貫貨貪貧貯貼貳貽賁費賀貴買貶貿貸賊資賈賄貲賃賂賅賓賑賒賠賞賦賤賬賭賢賣賜質賴賺賽購贅贈贊贏贍贓贖贗贛赤赧赦赫赭走赴赳起越超趁趙趕趟趣趨足趴趾跎距跋跚跑跌跛跆跡跟跨路跳跺跪跼踫踐踝踢踏踩踟蹄踱踴蹂踹踵蹉蹋蹈蹊蹙蹣蹦蹤蹼蹲躇蹶蹬蹺躉躁躅躂躊躍躑躡躪身躬躲躺軀車軋軍軌軒軔軛軟軻軸軼較載軾輊輔輒輕輓輝輛輟輩輦輪輜輻輯輸轄輾轂轅輿轉轍轔轎轟轡辛辜辟辣辨辦辭辯辰辱農迂迆迅迄巡迎返近述迦迢迪迥迭迫送逆迷退迺迴逃追逅這逍通逗連速逝逐逕逞造透逢逖逛途逮逵週逸進運遊道遂達逼違遐遇遏過遍遑逾遁遠遘遜遣遙遞適遮遨遭遷遵遴選遲遼遺避遽還邁邂邀邇邊邐邏邑邕邢邪邦那邵邸邱郊郎郁郡部郭都鄂郵鄉鄒鄙鄰鄭鄧鄱鄹酉酋酊酒配酌酗酣酥酬酪酩酵酸酷醇醉醋醃醒醣醞醜醫醬醺釀釁采釉釋里重野量釐金釘針釗釜釵釦釣釧鈔鈣鈕鈉鈞鈍鈐鈷鉗鈸鈽鉀鈾鉛鉋鉤鉑鈴鉸銬銀銅銘銖鉻銓銜鋅銻銷鋪鋤鋁銳銼鋒錠錶鋸錳錯錢鋼錫錄錚錐錦鍍鎂錨鍵鍊鍥鍋錘鍾鍬鍛鍰鎔鎊鎖鎢鎳鎮鏡鏑鏟鏃鏈鏜鏝鏖鏢鏍鏘鏤鏗鐘鐃鏽鐮鐳鐵鐺鐸鐲鑄鑑鑒鑣鑠鑲鑰鑽鑾鑼鑿長門閂閃閉閔閏開閑間閒閘閡閨閩閣閥閤閭閱閻闊闋闌闈闆闔闖闐關闡闢阜阡防阮阱阪陀阿阻附限陋陌降院陣陡陛陝除陪陵陳陸陰陴陶陷隊階隋陽隅隆隍陲隘隔隕隙障際隧隨險隱隴隸隻雀雁雅雄雋集雇雍雉雌雕雖雜雙雛雞離難雨雪雯雲雷電雹零需霄霆震霉霎霑霖霍霓霏霜霞霪霧霸霹露霽霾靂靈靄青靖靛靜非靠靡面靦靨革靴靶靼鞅鞍鞋鞏鞘鞠鞣鞦鞭韃韁韆韋韌韓韜韭音章竟韶韻響頁頂頃項順須預頑頓頊頒頌頗領頡頰頸頻頷頭頹頤顆額顏題顎顓類願顛顧顫顯顰顱風颯颱颳颶颺颼飄飛食飢飧飪飯飩飲飭飼飴飽飾餃餅餌餉養餓餒餘餐館餞餛餡餵餾餿餽饅饒饑饜饞首香馥馨馬馮馭馳馱馴駁駝駐駟駛駑駕駒駙駭駢駱騁駿騎騖騙騫騰騷驅驃驀騾驕驚驛驗驟驢驥驪骨骯骰骷骸骼髏髒髓體高髦髮髯髻髭鬃鬆鬍鬚鬢鬥鬧鬨鬱鬲鬼魁魂魅魄魏魔魘魚魷魯鮑鮮鮫鮪鯊鯉鯽鯨鯧鰓鰍鰭鰥鱉鰱鰾鰻鱔鱗鱖鱷鱸鳥鳩鳴鳶鳳鴆鴉鴕鴣鴦鴨鴒鴛鴻鴿鵑鵝鵠鶉鵡鵲鵪鵬鶯鶴鷂鷓鷗鷥鷹鷺鸚鸞鹹鹼鹽鹿麂麋麒麗麓麝麟麥麩麴麵麻麼麾黃黍黎黏黑墨默黔點黜黝黛黠黨黯黴黷鼇鼎鼓鼕鼙鼠鼬鼴鼻鼾齊齋齒齟齣齡齜齦齬齪齷齲龍龔龜";
        internal static void Start() {
            //載入font3DMaterial，為了解決UI文字穿透其他物品的問題
            //因為文字無法編輯Shader，只能做Material來用
            if (AssetBundle.LoadFromMemory(Properties.Resources.text) is AssetBundle assetBundle) {
                Font3DMaterial = assetBundle.LoadAsset<Material>("Font3DMaterial");
                Font3DMaterial.color = Color.white;
                assetBundle.Unload(false);
            } else {
                Logger.Log(LogLevel.Error, "[KK_STP] Load assetBundle faild");
            }
        }

        /// <summary>
        /// 取得動態字體清單
        /// </summary>
        /// <returns>動態字體清單</returns>
        public static List<string> GetDynamicFontNames() {
            if (DisablePreview) {
                return FontList.ToList();
            } else {
                return DynamicFonts.Keys.ToList();
            }
        }

        /// <summary>
        /// 建立動態字體字典，如果總數超過500則用到時再生成，否則一次建立完畢
        /// </summary>
        /// <returns>字典內已生成的字體總數</returns>
        internal static int CreateDynamicFonts() {
            DynamicFonts.Clear();
            List<string> fontlist = Font.GetOSInstalledFontNames().ToList();

            for (int i = 0; i < fontlist.Count; i++) {
                if (i > 0 && fontlist[i].Replace("Bold", "").Replace("Italic", "").TrimEnd().Equals(fontlist[i - 1])) {
                    fontlist.RemoveAt(i);
                    i--;
                    continue;
                }
            }
            FontList = fontlist.ToArray();
            if (fontlist.Count >= 500) {
                DynamicFonts.Add("Arial", Resources.GetBuiltinResource<Font>("Arial.ttf"));
                Logger.Log(LogLevel.Info, $"[KK_STP] Detact {fontlist.Count} fonts in your system.");
                Logger.Log(LogLevel.Info, $"[KK_STP] Based on Unity's limitations, this number is more than that can be generated.");
                Logger.Log(LogLevel.Info, $"[KK_STP] I am sorry to tell you that I have to disable your fonts preview.");
            } else {
                DisablePreview = false;
                if (fontlist.Remove("Arial")) {
                    DynamicFonts.Add("Arial", Resources.GetBuiltinResource<Font>("Arial.ttf"));
                }
                foreach (var fontName in fontlist) {
                    DynamicFonts.Add(fontName, Font.CreateDynamicFontFromOSFont(new string[] { fontName, "Arial" }, 30));
                }
                Logger.Log(LogLevel.Info, $"[KK_STP] Generate {DynamicFonts.Count} System Fonts");
            }
            ////一次要求足夠的字串，可以擴大單一字體的texture內存大小，但是做太多會爆掉
            //string c = chineseTxt.Substring(0, 2000);
            //foreach (var kv in DynamicFonts) {
            //    kv.Value.RequestCharactersInTexture(c);
            //    Logger.Log(LogLevel.Debug, $"[KK_STP] Generate {kv.Key}: {kv.Value.material.mainTexture.width}, {kv.Value.material.mainTexture.height}");
            //}
            return DynamicFonts.Count;
        }

        /// <summary>
        /// 取得字體
        /// </summary>
        /// <param name="fontName">字體名稱</param>
        /// <returns></returns>
        public static Font GetFont(string fontName) {
            if (!CheckFontInOS(fontName)) {
                Logger.Log(LogLevel.Message, $"[KK_STP] Cannot find {fontName} in your System.");
                FallbackFont();
            } else if (DynamicFonts.Count >= 499) {
                Logger.Log(LogLevel.Message, $"[KK_STP] Based on Unity's limitations, you can't generate more than 500 different fonts at the same time.");
                Logger.Log(LogLevel.Message, $"[KK_STP] Please restart Studio.");
                FallbackFont();
            }
            if (!DynamicFonts.ContainsKey(fontName)) {
                DynamicFonts.Add(fontName, Font.CreateDynamicFontFromOSFont(new string[] { fontName, "Arial" }, 30));
            }
            return DynamicFonts[fontName];

            void FallbackFont() {
                if (CheckFontInOS("MS Gothic")) {
                    Logger.Log(LogLevel.Message, $"[KK_STP] Fallback to MS Gothic");
                    fontName = "MS Gothic";
                } else {
                    Logger.Log(LogLevel.Message, $"[KK_STP] Use Unity BuiltinResource Arial.");
                    fontName = "Arial";
                }
            }
        }

        /// <summary>
        /// 字型設定項目
        /// </summary>
        public enum Config {
            All = 0,
            Font,
            FontSize,
            FontStyle,
            Color
        }

        /// <summary>
        /// 創建設定資料夾結構，或是變更設定中的特定項目
        /// </summary>
        /// <param name="textTreeNodeObject">要變更的OCIFolder.treeNodeObject</param>
        /// <param name="config">要設定的項目</param>
        public static void MakeAndSetConfigStructure(TreeNodeObject textTreeNodeObject, Config config = Config.All) {
            Patches.isCreatingTextStructure = true;
            OCIFolder textOCIFolder = Studio.Studio.GetCtrlInfo(textTreeNodeObject) as OCIFolder;
            TextMesh t = textOCIFolder.objectItem.GetComponentInChildren<TextMesh>(true);
            MeshRenderer m = textOCIFolder.objectItem.GetComponentInChildren<MeshRenderer>(true);
            TreeNodeCtrl treeNodeCtrl = Singleton<Studio.Studio>.Instance.treeNodeCtrl;

            TreeNodeObject nConfig = doMain(Patches.TextConfigPrefix, "", textTreeNodeObject);
            if (config == Config.Font || config == Config.All)
                doMain(Patches.TextConfigFontPrefix, t.font.name, nConfig);
            if (config == Config.FontSize || config == Config.All)
                doMain(Patches.TextConfigFontSizePrefix, (t.characterSize * 500).ToString(), nConfig);
            if (config == Config.FontStyle || config == Config.All)
                doMain(Patches.TextConfigFontStylePrefix, t.fontStyle.ToString(), nConfig);
            if (config == Config.Color || config == Config.All)
                doMain(Patches.TextConfigColorPrefix, '#' + ColorUtility.ToHtmlStringRGBA(m.material.color), nConfig);

            Patches.isCreatingTextStructure = false;

            TreeNodeObject doMain(string prefix, string value, TreeNodeObject nRoot) {
                TreeNodeObject node = nRoot.child?.Where((x) =>
                    Studio.Studio.GetCtrlInfo(x).objectInfo.kind == 3 &&
                    (Studio.Studio.GetCtrlInfo(x) is OCIFolder y) &&
                    y.name.Contains(prefix)
                ).FirstOrDefault();
                OCIFolder folder;
                if (null == node) {
                    //沒有找到就創建
                    folder = AddObjectFolder.Add();
                    treeNodeCtrl.SetParent(folder.treeNodeObject, nRoot);
                    folder.objectInfo.changeAmount.Reset();
                    node = folder.treeNodeObject;
                } else {
                    folder = Studio.Studio.GetCtrlInfo(node) as OCIFolder;
                }
                folder.name = folder.objectItem.name = prefix + value;
                return node;
            }
        }

        /// <summary>
        /// 讀取資料夾結構內的設定值
        /// </summary>
        /// <param name="textTreeNodeObject">要讀取的OCIFolder.treeNodeObject</param>
        /// <param name="config">要讀取的項目</param>
        /// <returns></returns>
        public static string GetConfig(TreeNodeObject textTreeNodeObject, Config config) {
            if (null == textTreeNodeObject) {
                textTreeNodeObject = (from v in Singleton<GuideObjectManager>.Instance.selectObjectKey
                                      select Studio.Studio.GetCtrlInfo(v) as OCIFolder into v
                                      where v != null
                                      select v.treeNodeObject).FirstOrDefault();
            }
            OCIFolder textOCIFolder = Studio.Studio.GetCtrlInfo(textTreeNodeObject) as OCIFolder;
            TextMesh t = textOCIFolder.objectItem.GetComponentInChildren<TextMesh>(true);
            MeshRenderer m = textOCIFolder.objectItem.GetComponentInChildren<MeshRenderer>(true);
            TreeNodeObject GetChildNode(string prefix, TreeNodeObject nRoot) {
                return nRoot?.child?.Where((x) =>
                    Studio.Studio.GetCtrlInfo(x).objectInfo.kind == 3 &&
                    (Studio.Studio.GetCtrlInfo(x) is OCIFolder y) &&
                    y.name.Contains(prefix)
                ).FirstOrDefault();
            }

            TreeNodeObject nConfig = GetChildNode(Patches.TextConfigPrefix, textTreeNodeObject);
            string GetValue(string prefix) {
                if (Studio.Studio.GetCtrlInfo(GetChildNode(prefix, nConfig)) is OCIFolder f) {
                    return f.name.Replace(prefix, "");
                } else {
                    return "";
                }
            }

            switch (config) {
                case Config.Font:
                    return GetValue(Patches.TextConfigFontPrefix);
                case Config.FontSize:
                    return GetValue(Patches.TextConfigFontSizePrefix);
                case Config.FontStyle:
                    return GetValue(Patches.TextConfigFontStylePrefix);
                case Config.Color:
                    return GetValue(Patches.TextConfigColorPrefix);
                default:
                    return "";
            }
        }

        /// <summary>
        /// 在給定的OCIFolder GameObject下添加TextMesh
        /// </summary>
        /// <param name="folder">要添加TextMesh的OCIFolder</param>
        /// <param name="text">預設文字</param>
        /// <returns>新建立的TextMesh</returns>
        public static TextMesh MakeTextObj(OCIFolder folder, string text) {
            Patches.DrawConfigPanel();
            folder.objectItem.layer = 10;
            GameObject go = new GameObject();
            go.transform.SetParent(folder.objectItem.transform);
            go.layer = 10;
            go.transform.localPosition = Vector3.zero;
            TextMesh t = go.AddComponent<TextMesh>();
            t.fontSize = 500;
            t.anchor = TextAnchor.MiddleCenter;
            t.text = text;
            go.transform.localRotation = Quaternion.Euler(0f, 180f, 0f);
            SetFont(folder);
            SetCharacterSize(folder);
            SetColor(folder);
            SetFontStyle(folder);

            Logger.Log(LogLevel.Info, "[KK_STP] Create Text");
            return t;
        }

        /// <summary>
        /// 更改選取項目的字型
        /// </summary>
        /// <param name="fontName">字型名稱</param>
        public static void ChangeFont(string fontName) {
            OCIFolder[] folderArray = (from v in Singleton<GuideObjectManager>.Instance.selectObjectKey
                                       select Studio.Studio.GetCtrlInfo(v) as OCIFolder into v
                                       where v != null
                                       select v).ToArray();
            foreach (var oCIFolder in folderArray) {
                SetFont(oCIFolder, fontName);
                MakeAndSetConfigStructure(oCIFolder.treeNodeObject, Config.Font);
            }
        }

        /// <summary>
        /// 設定字型，預設為MS Gothic
        /// </summary>
        /// <param name="folder">對象OCIFolder</param>
        /// <param name="fontName">字型名稱</param>
        public static void SetFont(OCIFolder folder, string fontName = "MS Gothic") {
            Color color = folder.objectItem.GetComponentInChildren<MeshRenderer>(true).material.color;

            TextMesh textMesh = folder.objectItem.GetComponentInChildren<TextMesh>(true);
            textMesh.font = GetFont(fontName);
            textMesh.font.RequestCharactersInTexture(textMesh.text);
            folder.objectItem.GetComponentInChildren<MeshRenderer>(true).material = Font3DMaterial;
            folder.objectItem.GetComponentInChildren<MeshRenderer>(true).material.SetTexture("_MainTex", textMesh.font.material.mainTexture);
            folder.objectItem.GetComponentInChildren<MeshRenderer>(true).material.EnableKeyword("_NORMALMAP");
            SetColor(folder, color);
        }

        /// <summary>
        /// 檢查OS中是否有安裝給定字型
        /// </summary>
        /// <param name="fontName">字型名稱</param>
        /// <returns>系統是否能使用給訂字型</returns>
        public static bool CheckFontInOS(string fontName) {
            var i = Array.IndexOf(Font.GetOSInstalledFontNames(), fontName);
            if (i >= 0) {
                return true;
            }
            Logger.Log(LogLevel.Message, "[KK_STP] Missing font: " + fontName);
            return false;
        }

        /// <summary>
        /// 更改選取項目的字體大小
        /// </summary>
        /// <param name="size">字體大小</param>
        public static void ChangeCharacterSize(float size) {
            OCIFolder[] folderArray = (from v in Singleton<GuideObjectManager>.Instance.selectObjectKey
                                       select Studio.Studio.GetCtrlInfo(v) as OCIFolder into v
                                       where v != null
                                       select v).ToArray();
            foreach (var oCIFolder in folderArray) {
                SetCharacterSize(oCIFolder, size);
                MakeAndSetConfigStructure(oCIFolder.treeNodeObject, Config.FontSize);
            }
        }

        /// <summary>
        /// 設定字體大小，單位放大五百倍
        /// </summary>
        /// <param name="folder">對象OCIFolder</param>
        /// <param name="size">字體大小</param>
        public static void SetCharacterSize(OCIFolder folder, float size = 1) {
            folder.objectItem.GetComponentInChildren<TextMesh>(true).characterSize = 0.002f * size;
        }

        /// <summary>
        /// 更改選取項目的字型顏色
        /// </summary>
        /// <param name="color">字型顏色</param>
        public static void ChangeColor(Color color) {
            OCIFolder[] folderArray = (from v in Singleton<GuideObjectManager>.Instance.selectObjectKey
                                       select Studio.Studio.GetCtrlInfo(v) as OCIFolder into v
                                       where v != null
                                       select v).ToArray();
            foreach (var oCIFolder in folderArray) {
                SetColor(oCIFolder, color);
                MakeAndSetConfigStructure(oCIFolder.treeNodeObject, Config.Color);
                Patches.panel.GetComponentInChildren<Button>(true).image.color = color;
            }
        }

        /// <summary>
        /// 設定字型顏色
        /// </summary>
        /// <param name="folder">對象OCIFolder</param>
        /// <param name="color">字型顏色</param>
        public static void SetColor(OCIFolder folder, Color color = default) {
            if (color == default) {
                color = Color.white;
            }
            folder.objectItem.GetComponentInChildren<MeshRenderer>(true).material.color = color;
        }

        /// <summary>
        /// 更改選取項目的字體樣式
        /// </summary>
        /// <param name="style">字體樣式</param>
        public static void ChangeFontStyle(string style) {
            OCIFolder[] folderArray = (from v in Singleton<GuideObjectManager>.Instance.selectObjectKey
                                       select Studio.Studio.GetCtrlInfo(v) as OCIFolder into v
                                       where v != null
                                       select v).ToArray();
            foreach (var oCIFolder in folderArray) {
                SetFontStyle(oCIFolder, style);
                MakeAndSetConfigStructure(oCIFolder.treeNodeObject, Config.FontStyle);
            }
        }

        /// <summary>
        /// 設定字體樣式
        /// </summary>
        /// <param name="folder">對象OCIFolder</param>
        /// <param name="style">字體樣式</param>
        public static void SetFontStyle(OCIFolder folder, string style = "Normal") {
            try {
                if (style == "") {
                    throw new OverflowException();
                }
                folder.objectItem.GetComponentInChildren<TextMesh>(true).fontStyle = (FontStyle)Enum.Parse(typeof(FontStyle), style);
            } catch (OverflowException) {
                Logger.Log(LogLevel.Error, "[KK_STP] OverflowException: Please use a correct FontStyle.");
                Logger.Log(LogLevel.Error, "[KK_STP] Fallback to FontStyle.Normal");
                folder.objectItem.GetComponentInChildren<TextMesh>(true).fontStyle = FontStyle.Normal;
            }
        }
    }
}
