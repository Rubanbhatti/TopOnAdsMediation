//
//  ATBaseUnityWrapper.m
//  UnityContainer
//
//  Created by Martin Lau on 08/08/2018.
//  Copyright © 2018 Martin Lau. All rights reserved.
//

#import "ATBaseUnityWrapper.h"
#import "ATUnityUtilities.h"
@interface ATBaseUnityWrapper()
@property(nonatomic, readonly) NSMutableDictionary<NSString*, NSValue*> *callbacks;
@property(nonatomic, readonly) dispatch_queue_t callbackAccessQueue;
@end
@implementation ATBaseUnityWrapper
+(instancetype) sharedInstance {
    return nil;
}

-(instancetype) init {
    self = [super init];
    if (self != nil) {
        _callbacks = [NSMutableDictionary<NSString*, NSValue*> dictionary];
        _callbackAccessQueue = dispatch_queue_create("com.anythink.UnityPackage", DISPATCH_QUEUE_CONCURRENT);
    }
    return self;
}

-(void) setCallBack:(void (*)(const char *, const char *))callback forKey:(NSString *)key {
    __weak ATBaseUnityWrapper* weakSelf = self;
    if (callback != NULL && [key length] > 0)
        dispatch_barrier_async(_callbackAccessQueue, ^{
            weakSelf.callbacks[key] = [NSValue valueWithPointer:(void*)callback];
        });
}

-(void) removeCallbackForKey:(NSString *)key {
    __weak ATBaseUnityWrapper* weakSelf = self;
    if ([key length] > 0)
        dispatch_barrier_async(_callbackAccessQueue, ^{
            [weakSelf.callbacks removeObjectForKey:key];
        });
}

-(void(*)(const char*, const char *)) callbackForKey:(NSString*)key {
    __block void(*callback)(const char*, const char *) = NULL;
    if ([key length] > 0) {
        __weak ATBaseUnityWrapper* weakSelf = self;
        dispatch_barrier_sync(_callbackAccessQueue, ^{
            callback = (void(*)(const char*, const char *))[weakSelf.callbacks[key] pointerValue];
        });
    }
    return callback;
}

-(NSString*)scriptWrapperClass {
    return @"";
}

- (id)selWrapperClassWithDict:(NSDictionary *)dict callback:(void(*)(const char*, const char*))callback {
    return nil;
}

-(void) invokeCallback:(NSString*)callback placementID:(NSString*)placementID error:(NSError*)error extra:(NSDictionary*)extra {
    if ([self callbackForKey:placementID] != NULL) {
        if ([callback isKindOfClass:[NSString class]] && [callback length] > 0) {
            
            NSMutableDictionary *paraDict = [NSMutableDictionary dictionaryWithObject:callback forKey:@"callback"];
            
            NSMutableDictionary *msgDict = [NSMutableDictionary dictionary];
            
            if (![ATUnityUtilities isEmpty:extra]) {
                
                // 过滤SDK返回参数的 user_load_extra_data 中不支持的类型
                if (extra[kATUnityUserExtraDataKey] != nil) {
                    NSMutableDictionary *extraDictM = [NSMutableDictionary dictionaryWithDictionary:extra];
                    NSMutableDictionary *extraDataTemp = [NSMutableDictionary dictionary];
                    NSMutableDictionary *extraDataDictM = [NSMutableDictionary dictionaryWithDictionary:extra[kATUnityUserExtraDataKey]];
                    for (NSString *key in extraDataDictM.allKeys) {
                        if ([extraDataDictM[key] isKindOfClass:[NSString class]] || [extraDataDictM[key] isKindOfClass:[NSNumber class]]) {
                            [extraDataTemp setValue:extraDataDictM[key] forKey:key];
                        }
                    }
                    if ([extraDataTemp count]) {
                        [extraDictM setValue:extraDataTemp forKey:kATUnityUserExtraDataKey];
                    } else {
                        [extraDictM removeObjectForKey:kATUnityUserExtraDataKey];
                    }
                    extra = extraDictM;
                }
                
                if (extra[@"extra"] != nil) {
                    msgDict[@"extra"] = extra[@"extra"];
                    msgDict[@"rewarded"] = extra[@"rewarded"];
                } else {
                    msgDict[@"extra"] = extra;
                }
            }
            
            paraDict[@"msg"] = msgDict;
            
            if ([placementID isKindOfClass:[NSString class]] && ![ATUnityUtilities isEmpty:placementID]) {
                msgDict[@"placement_id"] = placementID;
            };
            
            if ([error isKindOfClass:[NSError class]]) {
                
                NSMutableDictionary *errorDict = [NSMutableDictionary dictionaryWithObject:[NSString stringWithFormat:@"%ld", error.code] forKey:@"code"];
                
                if (![ATUnityUtilities isEmpty:error.userInfo[NSLocalizedDescriptionKey]]) {
                    errorDict[@"desc"] = [NSString stringWithFormat:@"%@",error.userInfo[NSLocalizedDescriptionKey]];
                } else {
                    errorDict[@"desc"] = @"";
                }
                if (![ATUnityUtilities isEmpty:error.userInfo[NSLocalizedFailureReasonErrorKey]]) {
                    errorDict[@"reason"] = [NSString stringWithFormat:@"%@",error.userInfo[NSLocalizedFailureReasonErrorKey]];
                } else {
                    errorDict[@"reason"] = @"";
                }
                msgDict[@"error"] = errorDict;
            }
            
            [self callbackForKey:placementID]([self scriptWrapperClass].UTF8String, paraDict.jsonString.UTF8String);
        }
    }
}

- (NSArray *)jsonStrToArray:(NSString *)jsonString{
   
    
    NSError *error;
    NSArray *array = [NSArray array];
    
    @try {
        NSData *jsonData = [jsonString dataUsingEncoding:NSUTF8StringEncoding];

        array = [NSJSONSerialization JSONObjectWithData:jsonData
                                                            options:NSJSONReadingMutableContainers
                                                              error:&error];
        if(error){
            return [NSArray array];
        }
    } @catch (NSException *exception) {
        NSLog(@"jsonStrToArray --- exception:%@",exception);
    } @finally {}

    return array;
}

@end
