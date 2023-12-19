//------------------------------------------------------------------------------
// This code was generated by a tool.
//
//   Tool : MetaMask Unity SDK ABI Code Generator
//   Input filename:  Contracts.sol
//   Output filename: ContractsBacking.cs
//
// Changes to this file may cause incorrect behavior and will be lost when
// the code is regenerated.
// <auto-generated />
//------------------------------------------------------------------------------

#if UNITY_EDITOR || !ENABLE_MONO
using System;
using System.Numerics;
using System.Threading.Tasks;
using evm.net;
using evm.net.Models;

namespace Contracts
{
	public class TOD721Backing : Contract, TOD721
	{
		public string Address
		{
			get => base.Address;
		}
		public TOD721Backing(IProvider provider, EvmAddress address, Type interfaceType) : base(provider, address, interfaceType)
		{
		}
		public Task<TOD721> DeployNew()
		{
			var method = System.Reflection.MethodBase.GetCurrentMethod();
			return (Task<TOD721>) InvokeMethod(method, new object[] {  });
		}
		
		[EvmMethodInfo(Name = "approve", View = false)]
		public Task<Transaction> Approve(EvmAddress to, BigInteger tokenId, CallOptions options = default)
		{
			var method = System.Reflection.MethodBase.GetCurrentMethod();
			return (Task<Transaction>) InvokeMethod(method, new object[] { to, tokenId, options });
		}
		
		[EvmMethodInfo(Name = "balanceOf", View = true)]
		public Task<BigInteger> BalanceOf(EvmAddress owner, CallOptions options = default)
		{
			var method = System.Reflection.MethodBase.GetCurrentMethod();
			return (Task<BigInteger>) InvokeMethod(method, new object[] { owner, options });
		}
		
		[EvmMethodInfo(Name = "baseURI", View = true)]
		public Task<String> BaseURI()
		{
			var method = System.Reflection.MethodBase.GetCurrentMethod();
			return (Task<String>) InvokeMethod(method, new object[] {  });
		}
		
		[EvmMethodInfo(Name = "cost", View = true)]
		public Task<BigInteger> Cost()
		{
			var method = System.Reflection.MethodBase.GetCurrentMethod();
			return (Task<BigInteger>) InvokeMethod(method, new object[] {  });
		}
		
		[EvmMethodInfo(Name = "getApproved", View = true)]
		public Task<EvmAddress> GetApproved(BigInteger tokenId, CallOptions options = default)
		{
			var method = System.Reflection.MethodBase.GetCurrentMethod();
			return (Task<EvmAddress>) InvokeMethod(method, new object[] { tokenId, options });
		}
		
		[EvmMethodInfo(Name = "gift", View = false)]
		public Task<Transaction> Gift(BigInteger[] quantity, EvmAddress[] recipient, CallOptions options = default)
		{
			var method = System.Reflection.MethodBase.GetCurrentMethod();
			return (Task<Transaction>) InvokeMethod(method, new object[] { quantity, recipient, options });
		}
		
		[EvmMethodInfo(Name = "isApprovedForAll", View = true)]
		public Task<Boolean> IsApprovedForAll(EvmAddress owner, [EvmParameterInfo(Type = "address", Name = "operator")] EvmAddress @operator, CallOptions options = default)
		{
			var method = System.Reflection.MethodBase.GetCurrentMethod();
			return (Task<Boolean>) InvokeMethod(method, new object[] { owner, @operator, options });
		}
		
		[EvmMethodInfo(Name = "maxMint", View = true)]
		public Task<BigInteger> MaxMint()
		{
			var method = System.Reflection.MethodBase.GetCurrentMethod();
			return (Task<BigInteger>) InvokeMethod(method, new object[] {  });
		}
		
		[EvmMethodInfo(Name = "maxSupply", View = true)]
		public Task<BigInteger> MaxSupply()
		{
			var method = System.Reflection.MethodBase.GetCurrentMethod();
			return (Task<BigInteger>) InvokeMethod(method, new object[] {  });
		}
		
		[EvmMethodInfo(Name = "mint", View = false)]
		public Task<Transaction> Mint(BigInteger _mintAmount, CallOptions options = default)
		{
			var method = System.Reflection.MethodBase.GetCurrentMethod();
			return (Task<Transaction>) InvokeMethod(method, new object[] { _mintAmount, options });
		}
		
		[EvmMethodInfo(Name = "mintPresale", View = false)]
		public Task<Transaction> MintPresale(BigInteger _mintAmount, CallOptions options = default)
		{
			var method = System.Reflection.MethodBase.GetCurrentMethod();
			return (Task<Transaction>) InvokeMethod(method, new object[] { _mintAmount, options });
		}
		
		[EvmMethodInfo(Name = "name", View = true)]
		public Task<String> Name()
		{
			var method = System.Reflection.MethodBase.GetCurrentMethod();
			return (Task<String>) InvokeMethod(method, new object[] {  });
		}
		
		[EvmMethodInfo(Name = "owner", View = true)]
		public Task<EvmAddress> Owner()
		{
			var method = System.Reflection.MethodBase.GetCurrentMethod();
			return (Task<EvmAddress>) InvokeMethod(method, new object[] {  });
		}
		
		[EvmMethodInfo(Name = "ownerOf", View = true)]
		public Task<EvmAddress> OwnerOf(BigInteger tokenId, CallOptions options = default)
		{
			var method = System.Reflection.MethodBase.GetCurrentMethod();
			return (Task<EvmAddress>) InvokeMethod(method, new object[] { tokenId, options });
		}
		
		[EvmMethodInfo(Name = "paused", View = true)]
		public Task<Boolean> Paused()
		{
			var method = System.Reflection.MethodBase.GetCurrentMethod();
			return (Task<Boolean>) InvokeMethod(method, new object[] {  });
		}
		
		[EvmMethodInfo(Name = "payee", View = true)]
		public Task<EvmAddress> Payee(BigInteger index, CallOptions options = default)
		{
			var method = System.Reflection.MethodBase.GetCurrentMethod();
			return (Task<EvmAddress>) InvokeMethod(method, new object[] { index, options });
		}
		
		[EvmMethodInfo(Name = "presaleActive", View = true)]
		public Task<Boolean> PresaleActive()
		{
			var method = System.Reflection.MethodBase.GetCurrentMethod();
			return (Task<Boolean>) InvokeMethod(method, new object[] {  });
		}
		
		[EvmMethodInfo(Name = "presaleSet", View = false)]
		public Task<Transaction> PresaleSet(EvmAddress[] _addresses, BigInteger[] _amounts, CallOptions options = default)
		{
			var method = System.Reflection.MethodBase.GetCurrentMethod();
			return (Task<Transaction>) InvokeMethod(method, new object[] { _addresses, _amounts, options });
		}
		
		[EvmMethodInfo(Name = "presaleWhitelist", View = true)]
		public Task<BigInteger> PresaleWhitelist([EvmParameterInfo(Type = "address", Name = "")] EvmAddress addr, CallOptions options = default)
		{
			var method = System.Reflection.MethodBase.GetCurrentMethod();
			return (Task<BigInteger>) InvokeMethod(method, new object[] { addr, options });
		}
		
		[EvmMethodInfo(Name = "publicSaleActive", View = true)]
		public Task<Boolean> PublicSaleActive()
		{
			var method = System.Reflection.MethodBase.GetCurrentMethod();
			return (Task<Boolean>) InvokeMethod(method, new object[] {  });
		}
		
		[EvmMethodInfo(Name = "release", View = false)]
		public Task<Transaction> Release(EvmAddress account, CallOptions options = default)
		{
			var method = System.Reflection.MethodBase.GetCurrentMethod();
			return (Task<Transaction>) InvokeMethod(method, new object[] { account, options });
		}
		
		[EvmMethodInfo(Name = "release", View = false)]
		public Task<Transaction> Release(EvmAddress token, EvmAddress account, CallOptions options = default)
		{
			var method = System.Reflection.MethodBase.GetCurrentMethod();
			return (Task<Transaction>) InvokeMethod(method, new object[] { token, account, options });
		}
		
		[EvmMethodInfo(Name = "released", View = true)]
		public Task<BigInteger> Released(EvmAddress token, EvmAddress account, CallOptions options = default)
		{
			var method = System.Reflection.MethodBase.GetCurrentMethod();
			return (Task<BigInteger>) InvokeMethod(method, new object[] { token, account, options });
		}
		
		[EvmMethodInfo(Name = "released", View = true)]
		public Task<BigInteger> Released(EvmAddress account, CallOptions options = default)
		{
			var method = System.Reflection.MethodBase.GetCurrentMethod();
			return (Task<BigInteger>) InvokeMethod(method, new object[] { account, options });
		}
		
		[EvmMethodInfo(Name = "renounceOwnership", View = false)]
		public Task<Transaction> RenounceOwnership()
		{
			var method = System.Reflection.MethodBase.GetCurrentMethod();
			return (Task<Transaction>) InvokeMethod(method, new object[] {  });
		}
		
		[EvmMethodInfo(Name = "safeTransferFrom", View = false)]
		public Task<Transaction> SafeTransferFrom(EvmAddress from, EvmAddress to, BigInteger tokenId, CallOptions options = default)
		{
			var method = System.Reflection.MethodBase.GetCurrentMethod();
			return (Task<Transaction>) InvokeMethod(method, new object[] { from, to, tokenId, options });
		}
		
		[EvmMethodInfo(Name = "safeTransferFrom", View = false)]
		public Task<Transaction> SafeTransferFrom(EvmAddress from, EvmAddress to, BigInteger tokenId, Byte[] _data, CallOptions options = default)
		{
			var method = System.Reflection.MethodBase.GetCurrentMethod();
			return (Task<Transaction>) InvokeMethod(method, new object[] { from, to, tokenId, _data, options });
		}
		
		[EvmMethodInfo(Name = "setApprovalForAll", View = false)]
		public Task<Transaction> SetApprovalForAll([EvmParameterInfo(Type = "address", Name = "operator")] EvmAddress @operator, Boolean approved, CallOptions options = default)
		{
			var method = System.Reflection.MethodBase.GetCurrentMethod();
			return (Task<Transaction>) InvokeMethod(method, new object[] { @operator, approved, options });
		}
		
		[EvmMethodInfo(Name = "setBaseURI", View = false)]
		public Task<Transaction> SetBaseURI(String _newBaseURI, CallOptions options = default)
		{
			var method = System.Reflection.MethodBase.GetCurrentMethod();
			return (Task<Transaction>) InvokeMethod(method, new object[] { _newBaseURI, options });
		}
		
		[EvmMethodInfo(Name = "setCost", View = false)]
		public Task<Transaction> SetCost(BigInteger _newCost, CallOptions options = default)
		{
			var method = System.Reflection.MethodBase.GetCurrentMethod();
			return (Task<Transaction>) InvokeMethod(method, new object[] { _newCost, options });
		}
		
		[EvmMethodInfo(Name = "setMaxMint", View = false)]
		public Task<Transaction> SetMaxMint(BigInteger _newmaxMint, CallOptions options = default)
		{
			var method = System.Reflection.MethodBase.GetCurrentMethod();
			return (Task<Transaction>) InvokeMethod(method, new object[] { _newmaxMint, options });
		}
		
		[EvmMethodInfo(Name = "setMaxSupply", View = false)]
		public Task<Transaction> SetMaxSupply(BigInteger _newMaxSupply, CallOptions options = default)
		{
			var method = System.Reflection.MethodBase.GetCurrentMethod();
			return (Task<Transaction>) InvokeMethod(method, new object[] { _newMaxSupply, options });
		}
		
		[EvmMethodInfo(Name = "setPresaleSaleStatus", View = false)]
		public Task<Transaction> SetPresaleSaleStatus(Boolean _status, CallOptions options = default)
		{
			var method = System.Reflection.MethodBase.GetCurrentMethod();
			return (Task<Transaction>) InvokeMethod(method, new object[] { _status, options });
		}
		
		[EvmMethodInfo(Name = "setPublicSaleStatus", View = false)]
		public Task<Transaction> SetPublicSaleStatus(Boolean _status, CallOptions options = default)
		{
			var method = System.Reflection.MethodBase.GetCurrentMethod();
			return (Task<Transaction>) InvokeMethod(method, new object[] { _status, options });
		}
		
		[EvmMethodInfo(Name = "shares", View = true)]
		public Task<BigInteger> Shares(EvmAddress account, CallOptions options = default)
		{
			var method = System.Reflection.MethodBase.GetCurrentMethod();
			return (Task<BigInteger>) InvokeMethod(method, new object[] { account, options });
		}
		
		[EvmMethodInfo(Name = "supportsInterface", View = true)]
		public Task<Boolean> SupportsInterface([EvmParameterInfo(Type = "bytes4", Name = "interfaceId")] Byte[] interfaceId, CallOptions options = default)
		{
			var method = System.Reflection.MethodBase.GetCurrentMethod();
			return (Task<Boolean>) InvokeMethod(method, new object[] { interfaceId, options });
		}
		
		[EvmMethodInfo(Name = "symbol", View = true)]
		public Task<String> Symbol()
		{
			var method = System.Reflection.MethodBase.GetCurrentMethod();
			return (Task<String>) InvokeMethod(method, new object[] {  });
		}
		
		[EvmMethodInfo(Name = "tokenByIndex", View = true)]
		public Task<BigInteger> TokenByIndex(BigInteger index, CallOptions options = default)
		{
			var method = System.Reflection.MethodBase.GetCurrentMethod();
			return (Task<BigInteger>) InvokeMethod(method, new object[] { index, options });
		}
		
		[EvmMethodInfo(Name = "tokenOfOwnerByIndex", View = true)]
		public Task<BigInteger> TokenOfOwnerByIndex(EvmAddress owner, BigInteger index, CallOptions options = default)
		{
			var method = System.Reflection.MethodBase.GetCurrentMethod();
			return (Task<BigInteger>) InvokeMethod(method, new object[] { owner, index, options });
		}
		
		[EvmMethodInfo(Name = "tokenURI", View = true)]
		public Task<String> TokenURI(BigInteger tokenId, CallOptions options = default)
		{
			var method = System.Reflection.MethodBase.GetCurrentMethod();
			return (Task<String>) InvokeMethod(method, new object[] { tokenId, options });
		}
		
		[EvmMethodInfo(Name = "tokensOfOwner", View = true)]
		public Task<BigInteger[]> TokensOfOwner(EvmAddress owner, CallOptions options = default)
		{
			var method = System.Reflection.MethodBase.GetCurrentMethod();
			return (Task<BigInteger[]>) InvokeMethod(method, new object[] { owner, options });
		}
		
		[EvmMethodInfo(Name = "totalReleased", View = true)]
		public Task<BigInteger> TotalReleased(EvmAddress token, CallOptions options = default)
		{
			var method = System.Reflection.MethodBase.GetCurrentMethod();
			return (Task<BigInteger>) InvokeMethod(method, new object[] { token, options });
		}
		
		[EvmMethodInfo(Name = "totalReleased", View = true)]
		public Task<BigInteger> TotalReleased()
		{
			var method = System.Reflection.MethodBase.GetCurrentMethod();
			return (Task<BigInteger>) InvokeMethod(method, new object[] {  });
		}
		
		[EvmMethodInfo(Name = "totalShares", View = true)]
		public Task<BigInteger> TotalShares()
		{
			var method = System.Reflection.MethodBase.GetCurrentMethod();
			return (Task<BigInteger>) InvokeMethod(method, new object[] {  });
		}
		
		[EvmMethodInfo(Name = "totalSupply", View = true)]
		public Task<BigInteger> TotalSupply()
		{
			var method = System.Reflection.MethodBase.GetCurrentMethod();
			return (Task<BigInteger>) InvokeMethod(method, new object[] {  });
		}
		
		[EvmMethodInfo(Name = "transferFrom", View = false)]
		public Task<Transaction> TransferFrom(EvmAddress from, EvmAddress to, BigInteger tokenId, CallOptions options = default)
		{
			var method = System.Reflection.MethodBase.GetCurrentMethod();
			return (Task<Transaction>) InvokeMethod(method, new object[] { from, to, tokenId, options });
		}
		
		[EvmMethodInfo(Name = "transferOwnership", View = false)]
		public Task<Transaction> TransferOwnership(EvmAddress newOwner, CallOptions options = default)
		{
			var method = System.Reflection.MethodBase.GetCurrentMethod();
			return (Task<Transaction>) InvokeMethod(method, new object[] { newOwner, options });
		}
		
		[EvmMethodInfo(Name = "withdraw", View = false)]
		public Task<Transaction> Withdraw()
		{
			var method = System.Reflection.MethodBase.GetCurrentMethod();
			return (Task<Transaction>) InvokeMethod(method, new object[] {  });
		}
		
		[EvmMethodInfo(Name = "withdrawSplit", View = false)]
		public Task<Transaction> WithdrawSplit()
		{
			var method = System.Reflection.MethodBase.GetCurrentMethod();
			return (Task<Transaction>) InvokeMethod(method, new object[] {  });
		}
		
	}
}
#endif
