﻿using NBitcoin.DataEncoders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace NBitcoin.Tests
{
	public class ElementsTests
	{
		[Fact]
		public void TestAssetIssuance()
		{
			using(var builder = NodeBuilder.Create())
			{
				var alice = builder.CreateNode(false);
				var bob = builder.CreateNode(false);
				var aliceRPC = alice.CreateRPCClient();
				var bobRPC = bob.CreateRPCClient();
				builder.StartAll();
				var blocks = aliceRPC.Generate(100);
				Assert.Equal(100, blocks.Length);
				var issuance = aliceRPC.IssueAsset(1000, 500, false);

				var tx1 = aliceRPC.GetBlock(blocks[1]).Transactions.First();
				//var tx2 = aliceRPC.GetTransaction();
			}
		}


		[Fact]
		public void CanParseBlockHeader()
		{
			BlockHeader header = new BlockHeader("010000000000000000000000000000000000000000000000000000000000000000000000f4733a0e18ee9d316e95a63cae43f5e8e76bc89df43a4c8cadbe7ee88eed9c38dae5494d00000000015100");
			Assert.Equal("010000000000000000000000000000000000000000000000000000000000000000000000f4733a0e18ee9d316e95a63cae43f5e8e76bc89df43a4c8cadbe7ee88eed9c38dae5494d00000000015100", Encoders.Hex.EncodeData(header.ToBytes()));
			Assert.Equal(new uint256("92a066f2390d9570f06072767fa01612147aa072cd8cdb863d677035f775abf2"), header.GetHash());
		}

		[Fact]
		public void CanParseTransaction()
		{
			//{
			//	"txid": "8018ddf65d787e04b01e3a7be580f1b75aaf69b86e34df30155ee0bc081560b9",
			//  "vin": "0",
			//  "entropy": "ce8864016abcf4c9e859a0231b3ab04122469dd25b339bd32923dd96053ac40e",
			//  "asset": "565023e3020947b01d7ed78485b21d8bbfefef8a4709a966666c1900d973efc2",
			//  "token": "50959ba51cbf34387bdacb31801e91610a01a0d80d161cac98cd2721e53b87ae"
			//}

			var tx = new Transaction(_Tx);
			Assert.Equal(tx.ToHex(), _Tx);
			Assert.Equal(new uint256("8018ddf65d787e04b01e3a7be580f1b75aaf69b86e34df30155ee0bc081560b9"), tx.GetHash());
			Assert.Equal(new uint256("bfe3468e3295544cc3601b2766ea4810b3fb52baa3f40b784956a7eb8583928d"), tx.GetWitHash());
		}

		string _Tx = "01000000000101f4733a0e18ee9d316e95a63cae43f5e8e76bc89df43a4c8cadbe7ee88eed9c382a00008000fdffffff0000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000008024f7f1f24a8c1e1594c61dd51609a4c36162736655428cf16b0e012fddc7df508ea3bb513d8d045118d1cd97cf82cef9023c7475c90e84c0d20e677eb6aecc643040b9c0afbd0d8d4b9af4641034db807b2a2afaff247ee0cf0eda6e25ddb996f4380080b05d3a5694f529e543ddf8dfa87b9137b1fbcae7849571bfc67f2f939254121036e0f7bc374254fb37d760164b1c388fe45c4b9311785943319513198b0190c531976a914065cee6e5401c2aa67df530c79ef853b4d15754088ac0b5c2be2c9e0c50a6a8eb7f9021b63b8df213443329988ee4e1c3961044e95b55c08647d274877fdea724d1826b2a319264626f3c1ade2d536d0295e2cbb72d70b690226676c3b439afeb27faeefc24ed9a4493e22e105e9d2dc0bb0e0d90c198820c71976a9148743a9e154420466a8304da13696f6d87bb386f288ac0b044e18dabbf1b410a024df9a69b2208128e835b797bc46dd78661cd987dbd8a008cb83ba7984e72a9c9a511d67219c15d8f81fad284c6466bb1dd6fc091d4e975f02467a7ee946739d95ebe4cac5b306f29feec6c49a8a232e466cbad79e17020cec1976a9144bb1aa0b435721091b22fd8cd7aced78e6053e4088ac0121667c3dcc51290904a6a9eae27337e6ff5602d0deb5ca501f77be96de63f6090100000000000146900000fd040a401f8904db5c7f34968c0f0eadbacdda5d3577cfd362131dbfb65aef372fe97fb8aec2558a6a0352c58030949d4ff2c8a7b663b9b8959683f5bb45f4c3c6e771bca7dfc93fbe6fad8afb7ffddd9729a5ff9a5184dba3f295df9a3e85f5b61f05f19fe1266bca1b0eafb4deff2f6ecb909c7caa5582006f25df8edeaa882765bf3094a056b41940cc931efd290b2e4bc88435961423fa8ceef2ebfdc5fa0c736c227d815392ca8eb19129d06c8b7b3cc4a1388515128dd7f70409dd90652956a255a6995a9534cb7bc692dce1c55de0d294b19062e3734b93c9704fa7cb977d3a32dc0333ea28c6db1107ce3d5b6c79f92f9ecc411e962a04a228d8d446920f70bc5d35fd24c06bee0c413aff35b0ca477f8a11440cae0aab1c852f6ac1eb915f5c18ed0df5f3fd3d0851a74ec9a6db6a357b7039389a4d7d3c8db2742d44891db2fd830e16fcf9e4c59a6f7e468397730653a0595078513878af273729fdb73e56455b262ea90d4cc7ef829ef6f430c9c0d7780d62bb7d28384cb5265911338bf70c37b609f94553cbf4c4af58d83cbd676ab527d6bd92810cb74956851f180b4c9cd61c2946ae86403663a5adff26ea021ab883f48db7d49a4bd4d7d161498c20b567cbbbf2785b55fb07edeb1e67314c47febc6fee4b8e65c3f5bc16befcc9a1ed95d774e3aab26af06b7b09ce9964b10e071db5fa2b65a79003fa73a068eed9c6743e983c37035ab8c111de76338e2d9a6f9e83037aefd4d78361efc905d74b961513b9b5cb634c89f11f4871bad9754bfa68d3a0554518dc6a185787532d478ee9992af933a705d6dd16018968b495996cb6ddb4f0f1e97f68ef60181155957139cecf742f6d5a320621659a806fac80af951de953508701aa193ce341a208ac424c45b922a67b9e7ce00019e265ec0e58276679a0a502ea9215ea66281196cfcf4954622c190e885bb6f2ab895c79e95e8b9dd110aaef776df2be673a550eeea832415163ed8e560dfed697f68781d9e920c4f3baadc1637fff06ab54ff7bbfa79f477df4041f9c96a1229083771049f6ed3eeac4ebb17b4d8b5440f5eefe48cb849bf9639b1cb7ef00c9bd57ddf100edfe54d3b4a1ec7b1c95903589e31d5ea2dee12c8b11ca6f1f2389b52eba841913ddddd12403b712bd1cfb3919138d8be1a15ffb51c31def2ba12b600457510f6c43e46c99c6f712ce7808fa7c72280a7894c15c27e397357b31898599a1ffd91fd8a987553e1e874395f5469785386dc6d4de5c2911a276e199e9faf7f35bd3128b206794475f466e0840c8d8f167c5264c40655f6849f155336a259bfc14920c7660c917bac2e8421f32b43c8bb0e0f4e1c7d511d4a2a5c9ca1ac319944e36c6211707144f2da1e4ce9c9f0c3cca3f58e64c6ff1101fb0e4f530f7f91ae9cec072fab2405f6220e748d6d49cb7eb4ea9df1044c32e43e15f3908ba6bad4b1638d8b8e04039e9e8033759c05c777aea6936fbb67c36cd5453520e6626be69841d8c1b045e1f4f7045c441677484a812a4818cfce911709ab2f6a97e734ce3eac2ffb11e7bc5510e59ad633a650eafb8dc2095735bef67032e0427c33da597d04794447fbaf8ddfa0607e38a15b0b89f5475d26d3f8d16ecd86fa5dc518642d0dc7ccee9f6c6bc047193fd1482da11c05a37c8d5bf216daca886985c6ae730b0b00bb268297cd0b7f8b4da5755c9101dfee05f486107c0d593e65488daed39f43ddc1770419bd03c2c4750ee7a0a6b9a8c444eb1ae083ae850a79c7e58fd263afb3ee01832ca504594d5fef25c9f5dc0fe7d26ebbd8ffabbab3580e1e625f78fa287c8462367e378a4073a756a0c932b979dce00b46545f579904480dedb0250bf5cf7482730c41313ba7ba37b07096449c0505e25771dad63c3de84978fc7f423e020634ebd51a97ce85c3258760389767780c76fe2818e28577095823139a40e51116a2e247a3d3a29cad7f79ff660af56211ec7a47ffe51ede265bcfc2b97655c87905e761437dfc291a01335c6ffd051ff213c68b952dc5a68426fbe40d5100f94553139ba83ade47ff415b3961759494c5b77840a378bed963acc14996995b292c6e4fe5f2c82c80de5a9c97b6893d39a7fb0f6b070dd38cbf452a9178511d2f937edbb24e17ae769c27442c2cf823217015aee3d01d4a7b8a6df0866ef2b0817ccf170cf61ab99afc76b29c1aadf9a0af9702af6218789268af29d6886a3619682a68898445180c7b281c04cc37b5b199fad79c3e6a019ac4a067eaf205f011474fec82a2543158a5e953b45263f790ae34e8a0d33d7b7e45bdbf68dee5a0900a332aa3fb55fbe3642289d953129002d325303eaa8f980947458b8a1947b5ea3880d7a20db28b51cf0224b9c7c2c97a43173ab4e2fae61ea899c2015f0f4843563bb6fc8878dd7c507ff4b0ee1153be077e97fdb46b9504c0f31258c708e771dbb05ea3164523aa8c934b8859aee76ac74b3dc29673c5b675e693f3ec1e4fdcccbcdcae9b77738aef85eebdb4098ea28e26777637d0afa45dc6fd7ff7686afb13cb28516c4efc9d06c23fbf4e7efb65d7a3cf21ba54bdd95e0882698fa27a68e9477d08b8f0e0570ba7909cfd061bd60f2506a69feacc3821ba0cac8906658f4486bdc330510a36abdab99d889f3c89505f3881b8c473b796a07fa7dacd6bfe36ed9ef30d3088b2734fbad207101d3f02f246cdea9b11132cb1543f9f5c7f42a2ad6813412211f2f2e6c6a09736e819a3e64040665514ede285050dfc486068beffb1b34a29fffc82bf9be8e72cb05a05c5649081f5030a044cbe43d2ea90e1fa2b823db1d646e2ab7a35fdeadaf10be7f72e7a591c46c135ab15c10f008eee37f0a87612724d05cfad21378288ecaad2ea948880e23b28e32d64a26278ee057c57ec90043ed9201e66453e288dacd6e3e67bcf05bc15d616ced8476345c03a39d258d9f0d20b3b2d6d3eb6a835f2994ab432b072c15149f68f90c956975ee5a3f62537d9261928af18f25939e68caf5e83093c6b9e31a9b00e4a4f8d68d04a44fd340bc5e2c7f92ebbc25ef4ff9e929278ad3e6e4ab90d2a381ced0f75896fcba1522cf88686299feb7dc39d0125b96679ba43c00370f109192763f6f3276d947ce63a9d59dbec92d3bfef1448024caadc8c51149950dd53cf6075ba8be2052326714592c37b1dd4dc15efd9bf0462ed6019a2b10d9a716055540a63a1409467b216522220e5b85cb10526a177b2d993d5811b24c73ea65ec2f4dbbadafe8022024e343a80ef576b6ca8d55c6b4f264d9634c04abf1df2f15daccf9c90ec3675ce8a839f4d816e3d1a9e08f5fcdcd956ad742bdf677e571004604603a14eb7b1591fc800d6e9e82d3d449709d4342e1ca5224fd5ac268384448a483c365d703f0090166e62770a51007784b09e8d94721a069be8d0198f1e32fb8af02a688ee9b4cacc4f739a7f235008fffab5440c9529923501c4679217fd41155f0ab2e56436a8629194368cc822f2915d7df64fdcd1ba680bd2387eb84c334b5999840e72b27fba2877aa89426e6371fc3363b9659246380b17b93ad055cb5ca2609d34865a5496c1af3171f2ed15ff2776723fd040a401fc7762a23d9f5a4af07f493b21647d41984dd1f0aa4e259160f312b76c6ea51cc577bcb1c4e15aff5d0465e8a3a64beb0f65786f25df565f0a38cece7ac89b966d6a87189ceeed0d8b85367dd0f37a3ebdf28120190598593ec5d69154e2efd3b62069e17d6aaaee03010da96b83712969c9b1c83a01ee48ec47d9c15e9b00f16280f6d992fdfb35b9ce892202e267ea2215afd28d3c9c1d098e6430b0ce277226b652a7d4524f5ddfdbfd6d263af1674613b0dc8297f8eab6655a705455d49b84007559b58d548a8d928931c6717817b74a8548d9687facd8c43eb31d4da629987285820c826171ea8180ca9a9be0a7107acc74a44c3f9bcdfa45c43ea9ae668e166bd950f47c1551b740514ba0a2565ebda44f24c9eed21646b8d3f9676cdb418a27781cb7682d167ac211d30c86becf209a6954abe7032098eca6fcef838a1cdeaccfa23230a097eecb76d2eb690936e18b07b137bee3ed03574b1e1af123639719a6cf7f52907074fa890ac1d8643570655b89400fc05464d93b969f69b6b3d2fd5f7b0572d38da964ff601a6707dfb8dc5d2bf3590e1dfa927944afaa3e9e88a7c07535aa5b095c12d4ffee8738a18bdf07969d95bf985ac1ea7d3a0e78104946c0ec3cd2b1c9dc264aa02c1f2bcfe3820bdc26a8e90c0cfed8d9ec5e69afb50528a5efa58871cabdc2cfad2bf032371e3e6f30e982d0752afc3d297e7cd3d48e997dcf80742e373f18418f445c4224e0af7b19f140ffe3d44668196f66e0575057e37c8a428e2bb398727d141fa5196187dd2b7b2a6c6b9b4d2709fe2c732dd1a441c89a40da7ef95ede62072f5eec9cef5c00779e4746e10185637fa27f95b8712400bd2e2cab2b61c2710d8a1c22ad38d329d70f1b76f2ef8ecda082d41cb59b1dd19fc34f27612313c05f62e1c911ca56486377404a9dd88549b98ef60a40b88146790b29dec5d337469edffc7db43c3ef5e7c0b48907932fca1364e06884845f19fbab5c1662402c528d62407823bcf5c8b422899b501ea197046c1d940a5a14c0b7acd64321480409c337fb2b22df937f2a0b2a7ffe4c8160514d3d7d6a84fcb1f7adaeaea5d6bf65d3371462473c3efa3d6f947d6899b979fb4cf69ec405c62c4328595be9e257c8d39782f7f3caf1287e348a5539b0f14fc60fddec44a44b9d45f44c1134e4f88ce1a9a3e93a0d8045bc1d5892fec2a6bd8902eeae86a527d30641ac1ae04996c5516c0dbb3706d002da292ffd9230a9a992db3a1123edffebe48d6ce34ea2c7177da27731d0adb4aafc0d94fdc0871c9884c2b41ba4a9a0b7c648e701ec4464d0a54bd7f08dcee53bd93a740283f88d48155158675be87a50a96e1842f9fde9e8f5fc40c9bea600f0059b3a812cd523d523fb6ee380c2ae2d5169de55c03d7e36c7bbf1981cc4fbe0853cde4af72781a9a6c406d93ecb564782460de9aa552aae9b490cd2b20b44334d3ee03c211217649657bba026b5982f8bf73d745677240fc2d3ca089c793e68af864bdb082e85a406af6b87b73b0267ab3ae209ade9ffa4c43e14f8ab49c5ad7eb8ee75462ef98d0227d19e44b7f93d11f888eea976c3b9432d2828a99007557cf014b4b1054eee91837e5dbca5f156738d68b38faa872dced2f26462b91d318b16dadb2af9e5f11ea5b453bef12281e93227497b2228cda0288c5b06d1dfc50445ea713f1c95c77309ac6ed2f1eeb7db3e55d0a1b5b85c49d79b7580ff1caf7186cdb791d78b97d9b4667a28fe7b87f58b4c02af420da5b74a463f14feed78ab9cbfdb7304d52b585a3fa3168ddac49df1dbeafbf6e16a025012e4e1f0412890c1ac2386924ddf4f11e340ea3732c1f1265e82411fc8327a8d1de36f646cf37ec6fac5c3ac9af47897b6a26764bda9d52dac441aef13e47ed26dfbf265e03aa7ba0a5d1a641814c3b9e1983edc48f724e4a7744751f70b9fd1cac72d4bf23c91c5ecfc9e99eb49f8a1a79e841a597436818c28bd8b8e4324cf63e583489e5cd775884c00f7b6b2f8e3b09ac775b75f4e06b38bb7c3f76091cb8a154cad04b1785d553a893e44abb06883f76e3b95ab9b1409b121b1677026eacbdc1e0f9cf9372eb9d62abfbd170ab0971d71dda446ded11c95529039f8dac617d82888afb83e47581b3eb780d8bfc1c09ee75bf8ccbf184cde9dd22594a8dc03ca2beba2450fd8faac77631f0931219d7233ae0dda97e5053ab987ba6deb7e093a0f3c21edd40a98a8ef8add412e94c8f640288eb5b3ea36a282f3ce9c3f367633a4c1a1ab2c4567ccfe102e2b1b34dafb9245b9714d9e9b761ea95f6c045eba947748dc0a610dff93aa0f059306405ff4b70776335619c29e3f7edd1a1d5f97ba9d732086e179167c5efe6b4ffd4f53a2d17861c0bfb82a7e6845e591ddb52f69a36fa77868cd9a9ad428f1cee66aa0c15752274a0374fed020eca2e6050f47d5cce9ee077e959821b80b91892f37d9a5e12ede5300c0680f5ef069b2dfc7e725dcb82b3d7721c47832c32188fe528aae8ba6cfc866419616d62deee50aca8aaabd86ab3861a85036f2708feeb8d383ed00198536f3261e0fda920f63d76a837bec73ad423489592d31fe8e58330bfaa3fb93468557275c62aeab3a80344494064afad66d992316da4f23c8e9d591bf995a5659dce51d2aaa1b225bf96e78af534f000f303802e4a38f030311b474a17fcc78657dfe85bacdc75883c05113d25572ef00f9b5ebfe93915ce29ef199940e3b574354782d147e35d95247e256974a4c303e01dc595c337c3270880f27c030288f5b6fd70fbc3d1aaf1eb4b9aea509fd7c861be24c314f2ff2f7c94fb955887599c84fdba7a866213ec3099aa6fa4be3e9e316f475a5e3318d9a5346c61420766d1c77a2b274d193662c756d2be5c948558408b484912547f4434e30e87a78f8258c43b908ecdfcaf3132d3d5700eb2028923002a4affcaaa5c270ae33d95bb68d0da9ceef09ac75ce9ce31d9e7e33c6a82ea0d11e495e93ad899064da87c3f6e892f145c661881f199e628fd4a9ac829261208125f7ef01efff7a9cd39ec2fd7ccf1c312df394e29b3955969757abc5734c185eccbdf079e60437f5cb3bb60a775f0336196f17f05d91dad1d2b5ab46ef77710ad723db8e21c43d61b593df777ff8a4d81b4c72c582cee36dce9ed96fcc563ba03363278c7def6d305a4d9ddac1aa0659ac382e068a5060f9829b509f24803870d08e186205a46a134d162734ffdc5effee2196a16397da3f633615269f603a95c429989bf2c3fd0e896135632438a1a66e08e8d235bf8fd3100a9c61d3c00939bc860491c4ba80a7577ef9536764072d2e558046b8f05f53927d1b50e3e38326c24ac9a8581e3f2349e1fc096db3217ebede01e59f82f76a1079c4d3215c0c47646598bf26995795f04ac8410c91c686aa73889fd43c54146b62268fb06183725a979c774d1d139d67c12594f89960bddd9b41d40f5910ed24445a129be3e2fec06184afd44b452a6b6fe67f00f216dd6b455ba955240ecae9dd5877205b01e765dfd123ea40344521892924bfc5083b1d908a62fc78b1f3e8d9fec3d4f743359fca497ac10aac0356dce420083030007f88c37761e5bf0909811c98b7acac3b83154cef56189fdb41bee6b673d79120e2cfe8a5b9cfc1bfebe75ac44870e3913f61757ebb8d5b8e7378ae5cf373157a0e05b063ad86d4cbaaf88723519a722588aab2cf3c2edf5d3decd45da8632d1aabe4e4943c94afaf2548ea0f56471f13c87d3057b2d4f65ac06398826305043a4fd0c0a601f0000000000000001067e8889e8e698d6e6190d86d0ac59b63f75bbe3a512c3058a1a034e4f41077ffbab43566b5e0247acfbcb21cf6425be41ecc321cb1bbc51890c72faa979f1fe7aca011ffafd848516a50a09ac54134416580eb9b7b2d406b68891713f0aef92c871942e4f3d249a7eec56034101ddf66c693ea51b8ea95c7fab80753059b5c4991472ff8cdc166fd61eb6167538e138db2e700a4cc5edad978bd8345c0b33fb8e501e59770a111ee097ac209ef086003125308dc6b227beb80da6c4589841a3fef12931e3174b0b82fa3646ec3ded533b47ae4b866b6e07db51c82f4d706fbb4ed236169d18b6cba57ecaacb04658ae1dac20ca8cba490fe57c707b51074b49daad8f676c023d5884c221f4e01af80e12248e1bde59b6fe9ee1bf8e8c55c7e8ff61be53c876cff357912e63ff7555f7c3c29803e73ec6a8db32eec3e716659a9acd133f97da74f032007e375fbe3ce5599ee0234891462e0c3b912c5a6c8c4f625f7b240624573648da8ed6856db60786ad87981eef06cb77658a319c5dc859e7556421901b5f222e4424936d47848f94b31aa7a1d4aef94c0a2d1e4d74647381d3e26f6478eae76851b2e924cc1b9b91bd4d62bce3270e716ec91acc743c59d44be86d3e5e017b6e2273a9b47940c6bcc553da45848c087db274ec5b0f50a83ff9ae792519be02d31d58f5a54568f9af915c4d50a585b8077eac9cb11b0890d68e95361d91f7c6fa2d3f03346efb3c28a93cfb6c356fad7728b3105a7a7c65faf2eb08df48e3307f653e0f580835aeaf14853aa664faea11322243afc2abc17c056d45484080b847ede42be0ef19f8fd8fd9ab8c48fa486f96424d31a3591bb42f42a018f67736f2ea127a51a53c7d76251fc2bae0dc65ffead265e8ae783ae9f2856748e3f6b7530c4730471661d13feb1f34c42becc8fcc8c6d8d31033f0c95587fa0fd440edb6ec75ffa6762cf54b4c0d2a4b9879db4fcac0421eba22f988861edaf4b0b006837c4677959061e7362ad31d81ae81f5ae0e0aa4d6394c7f3cd0b1ecd6e1d2ef780260a4bfff922f168177adfb70249409581cc85509f8fd0f3cbf3763ca85c500faadbee9c8e19f0d3c74be17bb91ad472b68604e41a1593f39d71ad68b2cbd16abe93436b851e521709fcebe0d36ddbd1b0e9f3ab1a0f13596c598592b62113cd0783fcb7b9cc3ae3911aa458f62ffd7eb59315c9fa004afc3abf7047ef8e30b98ce070cc19236c794f55a011f53c0bf5739f8e1572d894c02efd40410c78406cad2427569751c8371cfc40f60cbdb047f55a2e48bf43e997f989dfaa1c1dcd5a607e7aa1c66891c4cf9d73bec7967348c522ad004ddd8afc1913f778ac65d8f5ee9d581ab68ab852aa09acbec6392bc7dc7d65eb9542bc23027c92f8c9fc5078f0591941f760454ff6cc39ca49a8e5d5669c9f4d8eb420b5a19ae11eb345577071b3dd2fd770e5c1f690f07e04790937733572c92bf54cfa05d1d2c931e48a19d7b087f94add9ce730eb2e3cd93f830b15e22717690f0a9e01d055431c6e6196d10d74b5ab8021fc2be9dca3086c9efe1810b8462510b180fb2cb490646fae49a02dbed66d355b207283ac27a2b01668ff5b317c1eef0916b97eac687545fb50040231ef80078a0a5f7ea06d5fbdc759ac26960040b02a68e6830c98352fa8c1b591bce0d5731155fad102d6762354dd7c8f7101ecd580557bcfb0b635ec1d07c0d42f49531c32f723d72b855b4d21d48c7644327047a87bd8a98bd6519299fddb4d638b990f1468bafae4997510064f89f63707653b5f458fc34b8f7ffd0a5ae06b38f5f73d4019258937aa18827961f929f53bd0b3769cc96080b5392870c6c95a680042c0dbc8eed0bfe3c8b46cd48f87f0e6e22e2d9475fb1b79d89b3662ad46f5ecdc16893746be558f58ab18336a43cc4d1a1b539e04568057ed223ee9d3b4c63695edafaa4b3493bae6a6aaec1041fbe2f0a9461af8e71a31662f043e5aa71682d364a2050a36ea11deedb238c54ea011abfd79aabc81eaa8d9a256aab918c8a8c1eeb86d21036c48023351c06e0e6c4302735a49a524fb4793bd8e55c7c1c84e2f483efbe2928b17a51322b2097d7d58c663ce67044eb421476d3421ccea0019334ca7346edb883b9693b112c6fdcb36934cd664ca601b40b1aa13004c7534fc9d1bacf2ab48a92fa49241074ddb5c4e91dba4fa2787c82e443a6bf3d71bf8d9a7b522262eb33d0739b7cd5187bf21e4219e6c8b5784e488e3045b0a63b2eb381000981a18b7d1daeb6c6b4d363c1dabb56d950b62d5f6449c7785afd8e59d6ed3f13010f7127027a92c011a4bb17ffbd345b525ae968572122b5b7e84dc76e5b29ccac52f691cd97e17308d18d873016313b1d11ca440e54cf230196c5b3f288ed73291611e3bd086c84152ae43d7a63b9baccbdc2909ca14a77a6820a4428a2f3e8bb2d2fc11a4e2f9159b722fcce7733fd914af6f8febb4676203a97c1558e8e16a2386a106aec181ebf349394fe3d706e65f74d81aa37941cea0315351797fe1371bbc7d92b89c59332ef2c9bdd7777f923dfd66b0111fed65571b38f91aa40e9f688b738aa1db0233b8687472172862343c1baa13beaaf9b874ded37580b7d92a038f13f26fb41770543723d41ce8c01ebcbd924d7d0afe7b0350c71de9ae5b1e4bcb8344e4988699f435bf887bd7e5c14c78e6458293373e1ef72140677ba33a009a70f7c563b81d649c20ee94e4cd51c62d77b33f295e0df0d58224daf0214b2ef0de0aed34c2a6cd8d2e6d9d0a50c12c7a217b7032a13609c5341dc6dd8da9ce57495283e272bf08a81845378a95d63a4e37a071997e27985695e69a0923b313748620b8ea496f67b45e46e5fd58b0d98b291c849ec72a7e1a5382cce32161a4447e8377183fb7ebcebbcb557d67432191bd4e5e040970416f22f41eb0d480923ae8d9956f327c9a39aaf48c638513e8f04f54fb050282643a96e2d2aa1b1a49e5a01075c5a6e61629f4da34c272dbf0150d73b9f9cec313b5283d73d1eeb860e051e0ed1b340096a5ea510603db1e3edcda82492d486d08a68437df02ccb399e25042b44dded699ad2654eedaba3398a21d51b3d02dd985b6a45db3feb976928790212340ea6e25bd140f82f857cc66795ba33448f0f7b76f7ec22f20ddbcf7f7c84278ff32d0594b0857ea26a01019ee2bea205cc4d03dccf1f6c080f7c5fe8ae27dc5120fe72baefc295e4db191ba2621b7051a04f1bb06bb476357ba033132d96cdfc27f883408d45d5238458fd68937eec838a57287c4c264ebf8d408df8d28cc547593fcdcf4214a6801d3d442ae0b6b3f4f08e975e9320023afd87dfe64aa22c41fff9d16b10cdc75e25ec5ae761d5cb85232eba6382fb4914f2fb316de59ccaaafd6fd25eb1d511256a2d05a4f04fc28ea223de3405e9f3f08fb1e12516296c5177af336e5aacfcf2ea9c7f42bc3e5f37b3708f68434ecd6e793715dfdebd7c4d3489613b60bb822a84703af1a0b8ec5a5773c7b30747c8aec61cce30e95d5b16611aaee5d09dcf6379b6f2c465716c178ff5526fd979b186c2742010ddc9b7b92349e2815843598a71ae82f08303000776c5a8e4049ec00520810f687f21a53920a6bde9265f712dacea4d40c77e93a9608ec7567256054c64d559cbe0de33913aa864f02fb76f4d0754e3e73e48fdb52cbde9fa654d718133b9f5160aa35b8a5b515de1b8a5a9cf13e5093b9efca5f4f3689e45e00f55c394052e00ea5df0e9cd0ad1ff432a4cc85f810e8e24579b4dfd0c0a601f0000000000000001e172ebbc5f632e0175406d0ddf932e4a35caa604b4abcde3edfff49a3f09f6f3273862f04bc42b8241793f527edeafcce84053a66e3d362c7333bb2e929fcfddf700c0f8667aeae73ed2e7deb33e955b4a332174dd6165b2b2c41f3826f090f4c4096eaeb7fdfa7f161e35540041c408b635d4431eefb5d69334826da601994e5d14c1754d5fd086522e6b0211aaef7eb8d0342c458efcc52e5ef641bc79baff40baa80f12db1fecf1ad73dd08e90c9da4785ad53b40ce3528ac9fd5144bac661630f9886e322fcf06521987c68ceb975ad95088874345ca6cf74538f0673dc6e2264dbe021786d8a73de7b70e6246b623dac284ef0168c2f6b53a07652a1c781b312b4c9cbc1260949afbf4fadecd8b5e46bd2deb84acb4cbd8bde4b7ade9cda9a5cb8a5587bdccf5fc860b57222e9ca3a2fa3e5a74c53c93af3970b29a451e4b8ad43e4e74c5d08ec349ba5880f1e6902365bccbd761b2463815fac2f0cbaad2ba7935d89ddc5cf52c0196bb9a31b3d179fa57359e47ab923cec0b44a95a374db62f0553a9ca29ff9a4a9b140458428acb1843413a6ff4eb62231f7681b05982a9c248a9cdd2278352cf5ababf7c482a53a3b6740c2851ff6b795997069f43c621e6c76be8900328def2435b356e1c51a08154455c4ecb3a66c738d311477b23e79ba910b9c7417a122d51c83e96fc0f2301bed15a1a2438cbb65494a8296779621340d32caaa524ea44b16a92769cab691e1aaf535774ccf15450532366f36246fff14d9df8731950a972cd3f38aa742c96bf13806d688a51c13b3d60e6be09cef3c6f6ac2213baf463ebac237baa881b4895bee2afeebc42ce239955b99c44ab19836c23676caf192e31c9b8631f7b77a397a7d2f908b889cca2fef3b65367c20012aad9d5f24f660bee9d8a36c794fc0b36acf66e9fac477c5eb89315067cf2d852da00f2f2f44a5faef3ac2e17047cb5e79a57d5accea7a0848f7a61e9c8fead46bd7a7070ffc4daefc9b668afa02360f69521f2b427710169c568add901aad2853f4ab8f8aeaaf1e462222ac46ac92db98f6e6617bc25c4117ce585bf2213b58ec5dcf39a95faecfecb0c0f730ecab1fb9a56148f357176a0fdd9d42a18fbc9b988b117a6534d370895a027cd482b2a80e673e73dc227d13ee4890170f3c713e4eda1186e311dc155c516b89a4304afd4b4d1f2a50c172da1ed17f77e16fb7660a9865b19d2bf3b83a7d685500dd292206121227242275d72040d9b256c9ded33fe9f2751adb38fa8de7efbf7c18b5a08429d8c103bdf727b25d55fc8748fe26aa27e04ac7d61559b0295364ee42860817debc0bb67123f8fab66abe3327703b6331d6a3d9c1b591d0e318b47acc5c85acb3f72c4add0eccf65e4bb0ecf75c9d9ca59d80493d41c28bdd6cfe714fa87c197979c2df56843ce6a104cbd405e54648296e0e24df8927320b78205db806f9aa28312ab07766daea4c7a5c961633ffa72457f8bb6405320462eb47a85c483a9246a908221c63b6554605374cafe94a3f079de36de180192aa8fc39b874bb96e9798170e808eeb6ed4e9839513dc849cd160f90d941a85fb2a66ba83b86f6c5592bcc5dd8ef7d02b218b9681ec59fce889ae2009eae039325838f5b707a249cf0b4ca908281f25232a5f4b69c5bd67dabb09c1886fb074292ec8b82dedca9200b6844048dcd336f567fece08fdf757580903a18eb3bf18fc2f040034392ba116786fe14bacc708836dd32bdd89f5988bf993152f441feff76e424f134e1f1ff5bfb165200b6cdea13834322076b2adedf20957e694400b5ca2c113147e6d882240450ba965166282a1de801162cbda8b16e2ecd0c461389e34ec4256fa35ee318c9856460392558bb1b52f43c7bcc55af1d75ab4922a2148232480e275224b793b6157eb51beef850c0c73e9eb1ac9dd7c3898445f4a0339f09f91a3994718993211e34d0ba4099cd031eb67b1c4ee152fd199888670e62ce686f9b653a0471a1340b6ceeb2ac83549fc3c57970012f5eef6e7d86c4ed549f3820d67e0a015cbbc17a77366573f5c24cd25f44d0e8167cd5c6210704b4943a229f1079b4bbe17ed9f080b952719cf51d2ed7e95457de170f4c3c048c0e2b3678f2618dddb673b0c5fabf0c5efe1aada3e7340d78bb518a4537345b5cfef9e685dbf92a3d006fd85fd576503c1cb01d22cb9e2424e3e3d65b83503286cca6503a21480aac427331a05f687662f71bccf5743e83107426deb93285357ff4d7d17681c18439aeee7273f637a28ab1502d3f8eac4ed895ffea7931ef54aed85889d5cbc8a0a85f5636bb28aab4ac1b8d59bbdf532be553e65ecc116656feae03eb6482aafc54c3b014143c8546ea5c9a3dd81704f7d014ec6580310a91ee8210c5fc1f423131eb702722201f8256d4786a978ebe2cae4a6f4c600e86cbed34a523425a7d25a2973165adefc632ab953595cdcb369be9759e6052e1d5981a65265226ea4490d0baa6d2a484180c4d7f2145d2223043172c1b9ef9bbfaef5cba897e0c3bcc8f3f8e35d98031baedc6c310cd61dd8b6d4d255cdb6980b4ca934206cb56cb2126ee6eb659b7f7ebb1eadaada5114d196dfefdb819f2e2fef0eeb32564105ff774c4cb40d2531b8f6848d73d5e9546babc50cf9d2f8f1e062b81f61d072a4ec614ce6c2fffcdd0b36b97d91431a418854dd195dbc636c32239c96aa5a06e2c31e72716b58a8e618c4b4860e7afb59476ab7c1c54927614fd0d8abd6c3180bb96f3863529415f32465ba79f6ba2961ff94d443678f88dc11c8c17db687789d4eb65d033598a67aaca137457d611415206533cc57ed22385c99e3a160aa068e2200b44a240925675c6b93d3b42ebed613affdeac13232d8e12f1e6fcdd7c6a0c13c1cdcbff1c708df9608c33c765097142b0dfd4bb94e5350844e775c4b8609319a9b934f53a14f1c5697aa5d0be8aff57025d22df9956c75f39349f947896b434b3f41d16a79dee5389722bee5a1dc04f83789c6943356dde70bb4d7b2aeb40d07adbcd150e410669b94c3a14a20a2896d40e5277704ca3faf206b0504cbcb3ec2b0c132b0649a7afab5e9ef958d4b1da3bb65feb684c92e8a1fb87d4c20a30e6791022bb4bb5ad025adde2f6e6578dac3dd584403ba1c47d622a2d7a600ce3dfae195db86bd86b66999816385a5fb173ede5791c2bab7882a04ae7b0b687f8a2fb00849df249aba8aa4d2418fcd25b39d79d3e2522811e19a8f7eebdf8786433042eb7dcee7172fce37373f96106bb3f58d19ff43f418ee36e2ea9d5d7e50d82a42d0de832798e10978328202ca4694e8ad2f74eb97175512ee9e23c4b77deafde15d8df93e65429b64fe1ce1791d0db360f57c1ddb0778139821dabdf7950ccbbf6d1b3365f03ab93d8c3fec901ec3c38b1d19de85d023f4b5dc8f0185a859978216fa5f6a4ba60262b3c11fd98f5306ebd199fc0f66b4840bfbd1ad50559702e5df554640887d907c819fac4f5f974b3d963ea6e730a248ffd9a42ed530ce382fcd0dd271366a451c75f61eb3e913f99de200b6ad3910d0f0d04e6284a5cd7abe9ff5785421638b4a676eb34ca69ff215e183030007789faf08a9a7f8663a41649a72ec77f3a08470c241b0adb38dd71e01e6d728828e8eb3544a8ab2db8d79b3efc5f7c315bffa312d461c4c58a7902d84a41fbb3fcea2e8440bae49c0efdd517bd65b8cc465b9969c462a0f8437381472316e33bd0af6d9d0ffe32185ee04116ee261b7000551d85bfa3624c73172277343e219f0fd2d0e602c00000000000000013f062383ae6219b821a6cc8c78efab293203bae7be8a47134d0ea4696fc863a0e782b4d913b2c4781656d10a982e62f2ec56126c498dc308c878b0bb813d5b88b18aa4cc732cf481e8ac4cad0d8294f969e43e2125f79dcc487c774e9e18677bafc0c84fff67856c59e7ef2c94b671d0d86af5e2c725ef76b101cef93240a6c7d2a5b7277f6579221e7c6c0d52c718e9e0dc8438ccaaab9b874a53fa96d04ecb466712f3c3f3a97e650891bc4389c9b4bf95a4889b4ab5fd4dbef8adb9f9f14e1b33590facc6d32651add6e3b5faae2700d20eaeb0fe8eae2fe05f09318228677cf7780baa95c57c081d73c2b9a78d6e6712b6510376e55059ab6d3aec2a97b4790ff7faddf2fefc8467120e2ee4be197ea8a6b4875d5e7f818c23c77b39cecd3c84f60c64ecdfd7a70359057347fb301dd4074cff964f0627dd54db950ac7a55025e169f069a88c26b57b8a25afa101532291fe38c33caf69f215da7ca299ec981cd873bbd029d92097ef98fb06beee1a56140d49b3f91f66feeabbba66851cd16fbcfc1d3b3c591bc1200e0d5d878bec59ed0b5c5cdd2ec775e7506800728ce68e02f7a0a76aed8bd69b215287ae968e44c740422a4827fef82a0f08cebcc6c2775432e9575f31db07b6f8c565ddfcdf86c97d3f5ee7d31092e7b533149d133f871eee826460a4e3bc3f73fa6dcde03a430b478a5e07ad6555392115b5cbcadf1fe6981d53099d68e4be425613a5b87212f50875799740d2a010d7c236e76427e7da3cae8f76b075b08395516b7e6f0de610f9ea28b4f11d41a1304e01698eb725315d225941f0a320dd0b4a6ba7b9e158c57d0469fbdcc603a70747c032b6583a7bc94240b1cde7b2608ee1161ff72e079680cf0a80ba55611421bd7555bd1a321085250951f509e5aace72683a9d13400c04ccd591d6f5d147f0e12357e6cd48f004b303acd823d3aaa756972a4797d2f2ece722751c8ffa3d092c80d32a85721e022e193c6b49f07dd73a1ac074f6e21b9d4b74f349c844668eb75e4be36ec1d46914f3de15d96066d371f3516f2bda4af5cfcc3f2e9886eed79d3db7dab12debcf17341e900d00f1a1de7832e0464d0f7a59b74ce4c7234817f2242663a693ef54f3cdb6b342faf002ba23261aea47be30e85921c2c7a6cdd6450da40f9114086a24dd43ec065a10e90f03930c7da99eed339476f55db39f21354fcecc84b40e24be9acbe9a266eaf1a095efd2b3f023da540ca7cf44c4b3e4bf0ff5124860942754b1b36785b62b5e509d95d1c9014615d0d4368b5b2ed950a4edf0e5b57c4a7f518f3991e740a62f962ac94aa3c2f9b04008ef42416e68d931d6eb2ad2427b143e7aeca19dafcbb5dc7cc22df06c663a3d9c0e3fb2f8a20b42e46e66775eac0d34f2a8bbaa15c484bf6a8a0a90019d707ba59b2a1a72e89776f723cac7ec5bf08a5b8bdb7a44f7952f881f9f1d3d8bb73dfbe2af44053ecdbfdbc35a1b8df77382068cdef546ccc161c121917b513c9457fb03e1e8cf439d0fc41eecfaecb47012df446b596191da6718d0ccdab92536bb04cc34bcde754e0a3e8ef1c555c5f77f79d8ee3b3be31f1fe3b37f7558745ebd85914ac1579e6e407e9497a7ba44f2ab3f867f654f19f0d52a747c2888f1ad120f22702c47742434937b8081cc901eaf07e9608c32e922240cc14125b2c904692580813ef99236695747123037b9410610e886e75a2b452d05f8ae6ea41772a6a34be83d130705f061aac498916cd59ada2b7b5c0582aa1d6c26a91ab9e69bf13b7aef85e264bae5c103f60891c969c4ed4f5149aec9265d84007a7b1feae5fe187c1c09543cadc74703fee2d9ff196b9e4541a915de7c143bbd46ff41eb0947a2b3543e503b8782d2d34c66940293e285008b52259bef155a762ba786aed9058e3eb0e6758ebe02e17c450eda1a820b74da074deb615506f6593fa2b6aed779a52877e634dd615da85b775cca79a33556a56b322357a65187299ce90f8f3b83d6261cab525086068a7ec2a7bbe8a6790c3f80cb033af2da9e41e057c8d7ed305a127027162b36e12475957f72d0506b857008d792476af212ba76a350dbc76a18b3cf8dc8b7dbedc7cf6a227154575c36d42800b537531df0ed24b9577cf6678ae446707cd705b6b43f58cb723b257f7bcd1f3d7e5e9393de46c490f052e32ced6ae59d7ddeddb7ef22b0f99c8e5da57079c23b16f7e8ad504e30994030dc33ab7e0c23779cb862132fd28c455f58e59bad33f39076cd3320d9fe62c6cf43de19d10dcf6ea532db8280e35a13deb5289fd508001eabe2e8c8f7640e01840f1d42bf60172cbd49ca15c1d579798f5c87d74b0cc5238cdd385a7ab8daf3f30afc24cbae30caa699116731d6e7108e64d2d4d190a5fd7b802763cd6ee6a339e9e6953caf9845cc71dcd4667908a3cc487190f190f03189fd762b61ca4108db24f8a445b2e3badb2776145f029c9c51186e0bfaad64b103c504c27b3dd62bb1c1ec1a6b79d08a03991637b1f8131f441ba74cd7d56ef34055cc9e57b940d91e8974d1b4d2a532a504ad1ffc6dcc926054cfc24f89736c839ccf1a7db0fc9ba41de266e74e3edff0220061774a089c06d567aa7cf3940512c0d85e4733dff4bbb559faed25ea6a8b72d139a41139807fe723916a32ad7cdb2fcadf3bed50774ea97021058de51d57fed4c14599de4fd695b25da7a485ba0ccded097dd1a4fa4e14b11f6fa7b53033759a9df0cb8d05e605c082e0ba97b533f2e85948cf754f45ac8bda7dcd18c74e215495faa79ea893c153b3531622a2bba2e19249f5f1288eaa41a440408a3804cdca6f08dc36521006370f848c53f996ce4a2469bca8d2dcc1bff5db1aca40bcd7359bce7724885868b771fe52b799439a24bfe9357524ca6801ea0bc35059f0596c7a6fcec963e5f836a43507e9d42a99c941695af8db790b5e4eeb10cd421d93638a365070f5acd0ae2d515999165bbcccd64e9d805673267a4001899cfc34f85ecda201db0d02d5a05be8a95d8ce0052460d84b4a173ccb32518994e3420cd97cbc3cdb3703a359c14c175fd8ba36b0f7e2b5abe5aad8e190ddcd1dab2e84bfa8020197dab38120f953d7528419ad091e34ad430890c04656d1f39207879b7851e63ee918a2091fbc088a023efa7e634f6bf335e24b46111f0bcfc320aaf21125974ec3c3d44a9acdf00e9dba82aa5ef4081c9f450d712454edd2237f2fdc26d20ad007c77882c87404910017b6dc937af46153f910b623b77c9524c3ea446ccaa98b2c0f8b44f87cc35784574d146f9eec6ca6c771c1beba18edcf610ac0c896ff06f11c721b4d2110f32d58e44a67729153fd7b2be980d524bcba3df3e999895031dbb95e14b4c10e800e99fed833284908e91366716a3ad4f1054eb622144da6e22a73c0660dfabf893d57a1eae755015d7f108114a1a1125a0f078403084f1fe00d51530fa30c2aa11e37a30ac302ef39346568ad59c5b50a689bba5574935cae24ccb49f0255dc99d33afcbb28f696de3c571791fbbf9f373119620aca5ecb2c84e33f083e57b1791f0ad13ee45638200a09dc65ee652a917313465ce0535ab6194aeaaf0652496b58e513d0033ab8714c72ac1130e2c08ee1b79e76c517ec316dac36bb75262b1bc421639614b5784ad35069a11918e42513c224767fa6d012a6a6234a8399e20bf1649bf3252ee7119d7f6b193d60908cabd46794d8799c93e62f425bdae51f0b39cc264768648bee3cfa4513dc0a41f13d29b099fcc27ceff4c90b4f6e0b380f6d7a5c7eb062db427c886b69be90b89f20a9989b1394690fa9eb5aaece587a19f5ce18874107dbe1607a0be554f0d35dbcf414b3944045a1e3ee63a496df37e860545be74e3507f429d89f19422102e864315f2b378c8a3c2715396c2ee0528922e90db0955cdbc2982c9a7516df8f9f6a40e31ed552d32ea1e376ab34b1b52c43b49947401d796d4c22ca31ca1b0ee8b65dc5ceead141d1acdc283009f2e02eeeeaa1e54d516c08d9762a7284d52bdc2944cc22bdc9ad170ad483b7c65ea2730b5b57ca18b3b5dc70511c0f22e98a6b729dbf5b4a93c14da697e73120739b5d600a198eb300fad21a6305ad8a8cabdd290834e5871fac3f650b37a83d8afb83ae2c143ae5fc77061517dd4c022bd90a38d74788327e76014abc393ac59d2afee44f812be23bfd535c871916514a01b476eb2eb6e947c463a1c57e7d5c5fd2a66d32cb64216cdd8a4409fe393ddd5cf102849f3268019276cae27db6236be2b3874b1187201065250c3ea671665364e5c87084e3033ff1eba011a2abdbb63f9d3907e0987134adc52ee8e34c48816b57150877afd90d2291c468c8d7e171104771682dbd7b4bbc766d581d9faec281ebd545a3118f8e0bb0d8fde9c75f931b1606b94027ca00ac0dcbe214fec4a808c8e5dba41f7b1c45d42af77c146917ef26a07dff66e43241c259bd650a8fc5e142d91d8c2629bd605845d6a5e4a7d1e018427c49bcf99fa03716da7b4cd99d43fe0ea70bfbd1c2d2fa3aaf786df891714e7b1d64a02bcb2cd22419453b93614520c44a7147e1c10bf57bfbc8813fb2b21afb608d1d90a270081b5f82cf28323a3f1bb6c6c0ae3124f7d0dfd136ed6231da8172272baf540658ee3f3d65548a4d8a4de058342e2f22c41bdc7cd67e3de4f3823bea461bee03e6233c8b77bfa62cc3f2e7e49a5c31ab814aaabe185725f03a28267812b5ee2f5c4c1bccf9bf8cfa5c56f6a92ea01d046a8e54f60ae63405f901cce500b0353af1dba3ecc87d7d4c1c259c46b5fb4a921e943f69e8c201d48462709f855c074c09111000a31b1e62c47e9f3e13a9f6dc4432a37bb339999422375d39fbbb7cbc047f6674f7e3d8b47dd2ca138a25c956f2550acb069fbdd03a8c676a5873d6305f6c5352aa86020c3d70d3b995141b3776105881e7eb4c84d6ad4687cd0adc4ff8ccc29db0dcb815dee0f5c5bacf5f1d16bfab4b41d7c26e27ed1c0b97692f343c00c9a01d7b4a9dde29ec9ae31ef705d7ca831c220cc96f119dd92ee2842fc1f26ccef039a6064f521801571285dcbfd7c2db8f38000064000000";
	}
}
